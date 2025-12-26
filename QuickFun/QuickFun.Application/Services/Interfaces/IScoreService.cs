using QuickFun.Application.DTOs;

namespace QuickFun.Application.Services.Interfaces;

public interface IScoreService
{
    Task<ScoreDto?> GetScoreByIdAsync(Guid id);
    Task<IEnumerable<ScoreDto>> GetPlayerScoresAsync(Guid playerId);
    Task<IEnumerable<ScoreDto>> GetGameScoresAsync(Guid gameId);
    Task<IEnumerable<ScoreDto>> GetHighScoresAsync(Guid gameId, int count);
    Task<ScoreDto> CreateScoreAsync(ScoreDto scoreDto);
    Task UpdateScoreAsync(ScoreDto scoreDto);
    Task DeleteScoreAsync(Guid id);
}