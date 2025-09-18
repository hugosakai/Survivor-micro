// _Scripts/Gameplay/Combat/CombatMock.cs
using System;
using System.Collections;
using UnityEngine;

public class CombatMock : MonoBehaviour, IHealth, IDamageDealer
{
    [SerializeField] private Health _enemy;
    public float CurrentHealth => throw new System.NotImplementedException();

    public float MaxHealth => throw new System.NotImplementedException();

    public void ApplyDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Heal(Heal amount)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        _enemy.OnHealthChanged += Enemy_OnHealthChanged;
        _enemy.OnDamaged += Enemy_OnDamaged;
        _enemy.OnHealed += Enemy_OnHealed;
        _enemy.OnDied += Enemy_OnDied;
        _enemy.OnInvulnerableHit += Enemy_OnInvulnerableHit;

        StartCoroutine(DamageApplier());
    }

    private void Enemy_OnInvulnerableHit(Damage damage, float arg2)
    {
        Debug.Log($"Hit invulnerable: {damage.ToString()} and timer: {arg2}");
    }

    private void Enemy_OnDied()
    {
        Debug.Log("Enemy Dead!");
    }

    private void Enemy_OnHealed(Heal obj)
    {
        Debug.Log($"Enemy Healed with {obj}!");
    }

    private void Enemy_OnDamaged(Damage obj)
    {
        Debug.Log($"Enemy Damaged with {obj}");
    }

    private void Enemy_OnHealthChanged(float arg1, float arg2)
    {
        Debug.Log($"Enemy Health Changed = {arg1}/{arg2}");
    }

    private IEnumerator DamageApplier()
    {
        // yield return new WaitForSeconds(5.0f);


        // Debug.Log("Teste de dano fraco com vida cheia (5 de dano):");
        // var damage = new Damage(5.0f, DamageType.Normal, DamageSource.Enemy);
        // _enemy.TakeDamage(damage);

        // yield return new WaitForSeconds(2.0f);

        // Debug.Log("Teste de dano fraco sem vida cheia (4 de dano):");
        // var damage2 = new Damage(1.0f, DamageType.Normal, DamageSource.Enemy);
        // _enemy.TakeDamage(damage2);


        // yield return new WaitForSeconds(1.0f);

        // Debug.Log($"Teste de cura acima do total (20 de cura):");
        // var heal = new Heal(20.0f, HealType.Instant, HealSource.Potion);
        // _enemy.Heal(heal);

        // yield return new WaitForSeconds(2.0f);

        Debug.Log("Teste de ataques em sequencia:");
        var damage0 = new Damage(1.0f, DamageType.Normal, DamageSource.Enemy);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);
        yield return new WaitForSeconds(0.05f);
        _enemy.TakeDamage(damage0);

        yield return new WaitForSeconds(2.0f);

        Debug.Log("Teste de cura fraca (3 de cura):");
        var heal2 = new Heal(3.0f, HealType.Instant, HealSource.Potion);
        _enemy.Heal(heal2);

        // yield return new WaitForSeconds(2.0f);

        // Debug.Log("Teste de dano overkill(25 de dano):");
        // var damage3 = new Damage(25.0f, DamageType.Normal, DamageSource.Enemy);
        // _enemy.TakeDamage(damage3);

    }
}
