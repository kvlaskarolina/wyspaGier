using AutoMapper;
using QuickFun.Application.DTOs;
using QuickFun.Application.Services.Interfaces;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Interfaces;
using QuickFun.Domain.ValueObjects;

namespace QuickFun.Application.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<PlayerDto?> GetPlayerByIdAsync(Guid id)
    {
        var player = await _playerRepository.GetByIdAsync(id);
        return _mapper.Map<PlayerDto>(player);
    }

    public async Task<PlayerDto?> GetPlayerByEmailAsync(string email)
    {
        var player = await _playerRepository.GetByEmailAsync(email);
        return _mapper.Map<PlayerDto>(player);
    }

    public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
    {
        var players = await _playerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PlayerDto>>(players);
    }

    public async Task<IEnumerable<PlayerDto>> GetTopPlayersAsync(int count)
    {
        var players = await _playerRepository.GetTopPlayersAsync(count);
        return _mapper.Map<IEnumerable<PlayerDto>>(players);
    }

    public async Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto)
    {
        var player = _mapper.Map<Player>(playerDto);
        player.PlayerName = PlayerName.Create(playerDto.PlayerName);
        player.CreatedAt = DateTime.UtcNow;

        var createdPlayer = await _playerRepository.AddAsync(player);
        return _mapper.Map<PlayerDto>(createdPlayer);
    }

    public async Task UpdatePlayerAsync(PlayerDto playerDto)
    {
        var player = _mapper.Map<Player>(playerDto);
        player.PlayerName = PlayerName.Create(playerDto.PlayerName);

        await _playerRepository.UpdateAsync(player);
    }

    public async Task DeletePlayerAsync(Guid id)
    {
        await _playerRepository.DeleteAsync(id);
    }
}