namespace QuickFun.Application.DTOs;

public class ScoreDto
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public Guid GameId { get; set; }
    public string GameName { get; set; } = string.Empty;
    public int Points { get; set; }
    public DateTime AchievedAt { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsHighScore { get; set; }
}