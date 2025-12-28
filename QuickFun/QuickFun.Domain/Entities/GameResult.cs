using QuickFun.Domain.Enums;

namespace QuickFun.Domain.Entities;
public class GameResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public GameType Type { get; set; }
    public int Score { get; set; }
    public DateTime PlayedAt { get; set; } = DateTime.Now;
}