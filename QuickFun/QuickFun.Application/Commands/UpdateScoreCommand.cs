using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Commands;

public class UpdateScoreCommand : IRequest<ScoreDto>
{
    public Guid ScoreId { get; set; }
    public int Points { get; set; }
    public TimeSpan Duration { get; set; }
}