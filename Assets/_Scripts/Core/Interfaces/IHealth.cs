// _Scripts/Core/Interfaces/IHealth.cs
/// <summary>
/// Contrato para qualquer entidade que possa receber e perder "vida".
/// </summary>
public interface IHealth
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    bool TakeDamage(Damage damage);
    void Heal(Heal amount);
}
