using QuickFun.Domain.Entities;

namespace QuickFun.Domain.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Player?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Player>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Player>> GetTopPlayersAsync(int count, CancellationToken cancellationToken = default);
    Task<Player> AddAsync(Player player, CancellationToken cancellationToken = default);
    Task UpdateAsync(Player player, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}