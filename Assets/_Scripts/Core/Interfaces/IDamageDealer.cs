/// <summary>
/// Contrato para qualquer entidade que possa causar "dano".
/// </summary>
public interface IDamageDealer
{
    void Damage(int amount);
}
