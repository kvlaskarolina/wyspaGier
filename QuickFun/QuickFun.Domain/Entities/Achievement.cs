namespace QuickFun.Domain.Entities;

public class Achievement
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public int Points { get; set; }
    public DateTime UnlockedAt { get; set; }
    public Guid PlayerId { get; set; }

    // Navigation property
    public Player Player { get; set; } = null!;
}