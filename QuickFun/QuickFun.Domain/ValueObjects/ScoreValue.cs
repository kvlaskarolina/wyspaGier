namespace QuickFun.Domain.ValueObjects;

public class ScoreValue
{
    public int Points { get; private set; }

    private ScoreValue(int points)
    {
        Points = points;
    }

    public static ScoreValue Create(int points)
    {
        if (points < 0)
            throw new ArgumentException("Score cannot be negative", nameof(points));

        return new ScoreValue(points);
    }

    public ScoreValue Add(int points)
    {
        return Create(Points + points);
    }

    public override string ToString() => Points.ToString();

    public override bool Equals(object? obj)
    {
        if (obj is ScoreValue other)
            return Points == other.Points;
        return false;
    }

    public override int GetHashCode() => Points.GetHashCode();

    public static bool operator >(ScoreValue left, ScoreValue right) => left.Points > right.Points;
    public static bool operator <(ScoreValue left, ScoreValue right) => left.Points < right.Points;
    public static bool operator >=(ScoreValue left, ScoreValue right) => left.Points >= right.Points;
    public static bool operator <=(ScoreValue left, ScoreValue right) => left.Points <= right.Points;
}