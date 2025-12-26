namespace QuickFun.Domain.ValueObjects;

public class GameDifficulty
{
    public string Level { get; private set; }
    public int Multiplier { get; private set; }

    private GameDifficulty(string level, int multiplier)
    {
        Level = level;
        Multiplier = multiplier;
    }

    public static GameDifficulty Easy => new("Easy", 1);
    public static GameDifficulty Medium => new("Medium", 2);
    public static GameDifficulty Hard => new("Hard", 3);
    public static GameDifficulty Expert => new("Expert", 5);

    public static GameDifficulty Create(string level)
    {
        return level.ToLower() switch
        {
            "easy" => Easy,
            "medium" => Medium,
            "hard" => Hard,
            "expert" => Expert,
            _ => throw new ArgumentException($"Invalid difficulty level: {level}")
        };
    }

    public override string ToString() => Level;
}