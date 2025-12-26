using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFun.Domain.Entities;

namespace QuickFun.Infrastructure.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.Description)
            .HasMaxLength(500);

        builder.Property(g => g.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(g => g.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasMany(g => g.GameSessions)
            .WithOne(gs => gs.Game)
            .HasForeignKey(gs => gs.GameId);

        builder.HasMany(g => g.Scores)
            .WithOne(s => s.Game)
            .HasForeignKey(s => s.GameId);
    }
}