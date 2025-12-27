namespace QuickFun.Domain.Entities;

public class PlayerSession
{
    public string PlayerName { get; set; } = "Anonim";
    public List<GameResult> History { get; set; } = new();
}