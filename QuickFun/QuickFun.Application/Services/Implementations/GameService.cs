using AutoMapper;
using QuickFun.Application.DTOs;
using QuickFun.Application.Services.Interfaces;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Enums;
using QuickFun.Domain.Interfaces;

namespace QuickFun.Application.Services.Implementations;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GameService(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<GameDto?> GetGameByIdAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        return _mapper.Map<GameDto>(game);
    }

    public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GameDto>>(games);
    }

    public async Task<IEnumerable<GameDto>> GetGamesByTypeAsync(GameType type)
    {
        var games = await _gameRepository.GetByTypeAsync(type);
        return _mapper.Map<IEnumerable<GameDto>>(games);
    }

    public async Task<IEnumerable<GameDto>> GetFeaturedGamesAsync()
    {
        var games = await _gameRepository.GetFeaturedGamesAsync();
        return _mapper.Map<IEnumerable<GameDto>>(games);
    }

    public async Task<GameDto> CreateGameAsync(GameDto gameDto)
    {
        var game = _mapper.Map<Game>(gameDto);
        game.CreatedAt = DateTime.UtcNow;

        var createdGame = await _gameRepository.AddAsync(game);
        return _mapper.Map<GameDto>(createdGame);
    }

    public async Task UpdateGameAsync(GameDto gameDto)
    {
        var game = _mapper.Map<Game>(gameDto);
        game.UpdatedAt = DateTime.UtcNow;

        await _gameRepository.UpdateAsync(game);
    }

    public async Task DeleteGameAsync(Guid id)
    {
        await _gameRepository.DeleteAsync(id);
    }
}