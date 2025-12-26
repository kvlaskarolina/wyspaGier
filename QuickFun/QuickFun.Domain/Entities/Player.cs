namespace QuickFun.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public PlayerName PlayerName { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public int TotalGamesPlayed { get; set; }
    public string AvatarUrl { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<Score> Scores { get; set; } = new List<Score>();
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
}