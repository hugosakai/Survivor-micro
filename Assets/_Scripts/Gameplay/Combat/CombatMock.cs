// _Scripts/Gameplay/Combat/CombatMock.cs
using UnityEngine;

public class CombatMock : MonoBehaviour, IHealth, IDamageDealer
{
    [SerializeField] private HealthConfig healthConfig;
    public float CurrentHealth => throw new System.NotImplementedException();

    public float MaxHealth => throw new System.NotImplementedException();

    public void ApplyDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Heal(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        var damage = new Damage(10.0f, DamageType.Normal, DamageSource.Enemy);
        Debug.Log(damage.Amount.ToString());

        Debug.Log(healthConfig.MaxHealth);
    }
}
