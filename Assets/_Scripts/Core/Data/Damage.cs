using System;

public readonly struct Damage
{
    public int Amount { get; }
    public DamageType Type { get; }

    public Damage(int amount, DamageType type)
    {
        Amount = Math.Max(0, amount);
        Type = type;
    }

}