using QuickFun.Application.DTOs;

namespace QuickFun.Application.Services.Interfaces;

public interface IPlayerService
{
    Task<PlayerDto?> GetPlayerByIdAsync(Guid id);
    Task<PlayerDto?> GetPlayerByEmailAsync(string email);
    Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
    Task<IEnumerable<PlayerDto>> GetTopPlayersAsync(int count);
    Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto);
    Task UpdatePlayerAsync(PlayerDto playerDto);
    Task DeletePlayerAsync(Guid id);
}