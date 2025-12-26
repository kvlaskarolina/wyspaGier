using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Queries;

public class GetLeaderboardQuery : IRequest<IEnumerable<ScoreDto>>
{
    public Guid GameId { get; set; }
    public int Count { get; set; } = 10;

    public GetLeaderboardQuery(Guid gameId, int count = 10)
    {
        GameId = gameId;
        Count = count;
    }
}