using QuickFun.Application.DTOs;
using QuickFun.Domain.Enums;

namespace QuickFun.Application.Services.Interfaces;

public interface IGameService
{
    Task<GameDto?> GetGameByIdAsync(Guid id);
    Task<IEnumerable<GameDto>> GetAllGamesAsync();
    Task<IEnumerable<GameDto>> GetGamesByTypeAsync(GameType type);
    Task<IEnumerable<GameDto>> GetFeaturedGamesAsync();
    Task<GameDto> CreateGameAsync(GameDto gameDto);
    Task UpdateGameAsync(GameDto gameDto);
    Task DeleteGameAsync(Guid id);
}