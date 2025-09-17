/// <summary>
/// Contrato para qualquer entidade que possa receber e perder "vida".
/// </summary>
public interface IHealth
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeDamage(int amount);
    void Heal(int amount);
}
