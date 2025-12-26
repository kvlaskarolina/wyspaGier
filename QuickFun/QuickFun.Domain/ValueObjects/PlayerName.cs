namespace QuickFun.Domain.ValueObjects;

public class PlayerName
{
    public string Value { get; private set; }

    private PlayerName(string value)
    {
        Value = value;
    }

    public static PlayerName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Player name cannot be empty", nameof(value));

        if (value.Length < 3 || value.Length > 20)
            throw new ArgumentException("Player name must be between 3 and 20 characters", nameof(value));

        return new PlayerName(value);
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        if (obj is PlayerName other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();
}