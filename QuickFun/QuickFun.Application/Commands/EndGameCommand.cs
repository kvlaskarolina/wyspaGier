using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Commands;

public class EndGameCommand : IRequest<ScoreDto>
{
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public int FinalScore { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsWin { get; set; }
}