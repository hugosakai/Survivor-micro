using System;

public readonly struct Heal
{
    public float Amount { get; }
    public HealType Type { get; }
    public HealSource Source { get; }

    public Heal(float amount, HealType type, HealSource source)
    {
        Amount = Math.Max(0, amount);
        Type = type;
        Source = source;
    }

    public override string ToString()
    {
        return $"Heal(Amount={Amount}, Type={Type}, Source={Source})";
    }
}
