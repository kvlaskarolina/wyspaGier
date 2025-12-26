using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Commands;

public class CreateGameCommand : IRequest<GameDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxPlayers { get; set; }
    public int MinPlayers { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
}