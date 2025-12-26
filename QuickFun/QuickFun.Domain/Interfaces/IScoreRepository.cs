using QuickFun.Domain.Entities;

namespace QuickFun.Domain.Interfaces;

public interface IScoreRepository
{
    Task<Score?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Score>> GetByPlayerIdAsync(Guid playerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Score>> GetByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Score>> GetHighScoresAsync(Guid gameId, int count, CancellationToken cancellationToken = default);
    Task<Score> AddAsync(Score score, CancellationToken cancellationToken = default);
    Task UpdateAsync(Score score, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}