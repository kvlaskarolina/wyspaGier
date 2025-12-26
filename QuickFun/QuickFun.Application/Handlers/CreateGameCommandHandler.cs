using AutoMapper;
using MediatR;
using QuickFun.Application.Commands;
using QuickFun.Application.DTOs;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Enums;
using QuickFun.Domain.Interfaces;

namespace QuickFun.Application.Handlers;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameDto>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            MaxPlayers = request.MaxPlayers,
            MinPlayers = request.MinPlayers,
            ThumbnailUrl = request.ThumbnailUrl,
            Status = GameStatus.Active,
            Type = GameType.TicTacToe, // Default, should be passed in command
            CreatedAt = DateTime.UtcNow
        };

        var createdGame = await _gameRepository.AddAsync(game, cancellationToken);
        return _mapper.Map<GameDto>(createdGame);
    }
}