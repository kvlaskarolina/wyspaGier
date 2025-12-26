using Microsoft.EntityFrameworkCore;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Interfaces;
using QuickFun.Infrastructure.Data;

namespace QuickFun.Infrastructure.Repositories;

public class ScoreRepository : IScoreRepository
{
    private readonly ApplicationDbContext _context;

    public ScoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Score?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Scores
            .Include(s => s.Player)
            .Include(s => s.Game)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Score>> GetByPlayerIdAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _context.Scores
            .Include(s => s.Game)
            .Where(s => s.PlayerId == playerId)
            .OrderByDescending(s => s.AchievedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Score>> GetByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _context.Scores
            .Include(s => s.Player)
            .Where(s => s.GameId == gameId)
            .OrderByDescending(s => s.Value.Points)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Score>> GetHighScoresAsync(Guid gameId, int count, CancellationToken cancellationToken = default)
    {
        return await _context.Scores
            .Include(s => s.Player)
            .Where(s => s.GameId == gameId)
            .OrderByDescending(s => s.Value.Points)
            .ThenBy(s => s.Duration)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<Score> AddAsync(Score score, CancellationToken cancellationToken = default)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync(cancellationToken);
        return score;
    }

    public async Task UpdateAsync(Score score, CancellationToken cancellationToken = default)
    {
        _context.Scores.Update(score);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var score = await _context.Scores.FindAsync(new object[] { id }, cancellationToken);
        if (score != null)
        {
            _context.Scores.Remove(score);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}