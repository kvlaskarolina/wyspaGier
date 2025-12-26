using AutoMapper;
using MediatR;
using QuickFun.Application.DTOs;
using QuickFun.Application.Queries;
using QuickFun.Domain.Interfaces;

namespace QuickFun.Application.Handlers;

public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameDto>>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GetAllGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GameDto>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GameDto>>(games);
    }
}