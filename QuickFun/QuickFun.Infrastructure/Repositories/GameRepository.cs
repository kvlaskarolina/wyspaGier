using Microsoft.EntityFrameworkCore;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Enums;
using QuickFun.Domain.Interfaces;
using QuickFun.Infrastructure.Data;

namespace QuickFun.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Games
            .Include(g => g.Scores)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Games
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetByTypeAsync(GameType type, CancellationToken cancellationToken = default)
    {
        return await _context.Games
            .Where(g => g.Type == type)
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetFeaturedGamesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Games
            .Where(g => g.Status == GameStatus.Featured)
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Game> AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);
        return game;
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await GetByIdAsync(id, cancellationToken);
        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}