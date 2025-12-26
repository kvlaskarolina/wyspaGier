namespace QuickFun.Application.DTOs;

public class PlayerDto
{
    public Guid Id { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public int TotalGamesPlayed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}