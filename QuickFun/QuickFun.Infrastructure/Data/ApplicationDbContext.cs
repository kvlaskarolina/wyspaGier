using Microsoft.EntityFrameworkCore;
using QuickFun.Domain.Entities;
using QuickFun.Infrastructure.Data.Configurations;

namespace QuickFun.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Score> Scores => Set<Score>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<GameSession> GameSessions => Set<GameSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());
    }
}