namespace QuickFun.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GameType Type { get; set; }
    public GameStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int MaxPlayers { get; set; }
    public int MinPlayers { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
    public ICollection<Score> Scores { get; set; } = new List<Score>();
}