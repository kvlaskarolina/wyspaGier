namespace QuickFun.Domain.Entities;

public class GameResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string GameName { get; set; } = string.Empty; // np. "Snake", "TicTacToe"
    public int Score { get; set; }
    public DateTime PlayedAt { get; set; } = DateTime.Now;
}