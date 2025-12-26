using AutoMapper;
using QuickFun.Application.DTOs;
using QuickFun.Application.Services.Interfaces;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Interfaces;
using QuickFun.Domain.ValueObjects;

namespace QuickFun.Application.Services.Implementations;

public class ScoreService : IScoreService
{
    private readonly IScoreRepository _scoreRepository;
    private readonly IMapper _mapper;

    public ScoreService(IScoreRepository scoreRepository, IMapper mapper)
    {
        _scoreRepository = scoreRepository;
        _mapper = mapper;
    }

    public async Task<ScoreDto?> GetScoreByIdAsync(Guid id)
    {
        var score = await _scoreRepository.GetByIdAsync(id);
        return _mapper.Map<ScoreDto>(score);
    }

    public async Task<IEnumerable<ScoreDto>> GetPlayerScoresAsync(Guid playerId)
    {
        var scores = await _scoreRepository.GetByPlayerIdAsync(playerId);
        return _mapper.Map<IEnumerable<ScoreDto>>(scores);
    }

    public async Task<IEnumerable<ScoreDto>> GetGameScoresAsync(Guid gameId)
    {
        var scores = await _scoreRepository.GetByGameIdAsync(gameId);
        return _mapper.Map<IEnumerable<ScoreDto>>(scores);
    }

    public async Task<IEnumerable<ScoreDto>> GetHighScoresAsync(Guid gameId, int count)
    {
        var scores = await _scoreRepository.GetHighScoresAsync(gameId, count);
        return _mapper.Map<IEnumerable<ScoreDto>>(scores);
    }

    public async Task<ScoreDto> CreateScoreAsync(ScoreDto scoreDto)
    {
        var score = _mapper.Map<Score>(scoreDto);
        score.Value = ScoreValue.Create(scoreDto.Points);
        score.AchievedAt = DateTime.UtcNow;

        var createdScore = await _scoreRepository.AddAsync(score);
        return _mapper.Map<ScoreDto>(createdScore);
    }

    public async Task UpdateScoreAsync(ScoreDto scoreDto)
    {
        var score = _mapper.Map<Score>(scoreDto);
        score.Value = ScoreValue.Create(scoreDto.Points);

        await _scoreRepository.UpdateAsync(score);
    }

    public async Task DeleteScoreAsync(Guid id)
    {
        await _scoreRepository.DeleteAsync(id);
    }
}