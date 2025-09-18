// _Scripts/Core/Data/Damage.cs
using System;

public readonly struct Damage
{
    public float Amount { get; }
    public DamageType Type { get; }
    public DamageSource Source { get; }

    public Damage(float amount, DamageType type, DamageSource source)
    {
        Amount = Math.Max(0, amount);
        Type = type;
        Source = source;
    }

    public override string ToString()
    {
        return $"Damage(Amount={Amount}, Type={Type}, Source={Source})";
    }
}