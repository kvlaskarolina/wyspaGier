namespace QuickFun.Domain.Entities;

public class GameSession
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public bool IsCompleted { get; set; }
    public string? SessionData { get; set; }

    // Navigation properties
    public Game Game { get; set; } = null!;
    public Player Player { get; set; } = null!;
}