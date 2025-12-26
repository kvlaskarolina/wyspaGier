using Microsoft.EntityFrameworkCore;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Interfaces;
using QuickFun.Infrastructure.Data;

namespace QuickFun.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _context;

    public PlayerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .Include(p => p.Scores)
            .Include(p => p.Achievements)
            .Include(p => p.GameSessions)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Player?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .Include(p => p.Scores)
            .Include(p => p.Achievements)
            .Include(p => p.GameSessions)
            .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<Player>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .Include(p => p.Scores)
            .OrderBy(p => p.PlayerName.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Player>> GetTopPlayersAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .Include(p => p.Scores)
            .OrderByDescending(p => p.TotalGamesPlayed)
            .ThenByDescending(p => p.Scores.Sum(s => s.Value.Points))
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<Player> AddAsync(Player player, CancellationToken cancellationToken = default)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync(cancellationToken);
        return player;
    }

    public async Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
    {
        _context.Players.Update(player);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var player = await _context.Players.FindAsync(new object[] { id }, cancellationToken);
        if (player != null)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}