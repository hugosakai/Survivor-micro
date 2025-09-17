// _Scripts/Core/Interfaces/IDamageDealer.cs
/// <summary>
/// Contrato para qualquer entidade que possa causar "dano".
/// </summary>
public interface IDamageDealer
{
    void ApplyDamage(Damage damage);
}
