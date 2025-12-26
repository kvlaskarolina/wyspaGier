namespace QuickFun.Domain.Entities;

public class Score
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public Guid GameId { get; set; }
    public ScoreValue Value { get; set; } = null!;
    public DateTime AchievedAt { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsHighScore { get; set; }

    // Navigation properties
    public Player Player { get; set; } = null!;
    public Game Game { get; set; } = null!;
}