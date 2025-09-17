//Contrato responsável pela Saúde. Tudo que possuir saúde precisa desse contrato.
public interface IHealth
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeDamage();
    void Heal();
}
