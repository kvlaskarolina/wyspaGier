using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFun.Domain.Entities;
using QuickFun.Domain.ValueObjects;

namespace QuickFun.Infrastructure.Data.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.OwnsOne(p => p.PlayerName, pn =>
        {
            pn.Property(n => n.Value)
                .HasColumnName("PlayerName")
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.HasMany(p => p.Scores)
            .WithOne(s => s.Player)
            .HasForeignKey(s => s.PlayerId);

        builder.HasMany(p => p.Achievements)
            .WithOne(a => a.Player)
            .HasForeignKey(a => a.PlayerId);

        builder.HasMany(p => p.GameSessions)
            .WithOne(gs => gs.Player)
            .HasForeignKey(gs => gs.PlayerId);
    }
}