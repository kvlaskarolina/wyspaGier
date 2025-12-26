using QuickFun.Domain.Entities;

namespace QuickFun.Domain.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> GetByTypeAsync(GameType type, CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> GetFeaturedGamesAsync(CancellationToken cancellationToken = default);
    Task<Game> AddAsync(Game game, CancellationToken cancellationToken = default);
    Task UpdateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}