using QuickFun.Domain.Enums;

namespace QuickFun.Application.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GameType Type { get; set; }
    public GameStatus Status { get; set; }
    public int MaxPlayers { get; set; }
    public int MinPlayers { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}