public interface IDamageSource
{
    float Amount { get; }
    DamageType Type { get; }
    DamageSource Source { get; }
}
