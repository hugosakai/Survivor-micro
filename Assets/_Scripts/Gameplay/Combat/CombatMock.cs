using UnityEngine;

public class CombatMock : MonoBehaviour, IHealth, IDamageDealer
{
    public int CurrentHealth => throw new System.NotImplementedException();

    public int MaxHealth => throw new System.NotImplementedException();

    public void Damage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Heal(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        var damage = new Damage(10, DamageType.Normal);
        Debug.Log(damage.Amount.ToString());
    }
}
