using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Queries;

public class GetPlayerScoresQuery : IRequest<IEnumerable<ScoreDto>>
{
    public Guid PlayerId { get; set; }

    public GetPlayerScoresQuery(Guid playerId)
    {
        PlayerId = playerId;
    }
}