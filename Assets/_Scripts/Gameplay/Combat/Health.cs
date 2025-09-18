using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] float invulnFeedbackCooldown = 0.15f; // em s
    private bool _isDead = false;
    private float _invulnDuration;      // tempo total de invulnerabilidade
    float invulnUntil = 0f;           // até quando está invulnerável
    float invulnFeedbackUntil = 0f;   // até quando o feedback está “silenciado”

    public event Action<float, float> OnHealthChanged;    //(current, max)
    public event Action<Damage> OnDamaged;     //(damage)
    public event Action<Heal> OnHealed;      //(heal)
    public event Action<Damage, float> OnInvulnerableHit;     //(damage, tempo restante de invulnerabilidade)
    public event Action OnDied;
    public float CurrentHealth { get; private set; }

    public float MaxHealth { get; private set; }

    public void Heal(Heal heal)
    {
        if (_isDead) return;

        CurrentHealth += heal.Amount;
        CurrentHealth = Math.Min(CurrentHealth, MaxHealth);

        OnHealed?.Invoke(heal);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public bool TakeDamage(Damage damage)
    {
        if (_isDead) return false;

        // 1) checar i-frames
        if (Time.time < invulnUntil)
        {
            // ainda invulnerável → opcional: emitir evento de “hit ignorado”
            if (Time.time >= invulnFeedbackUntil)
            {
                float remaining = invulnUntil - Time.time;   // tempo restante de i-frames
                OnInvulnerableHit?.Invoke(damage, remaining);     // dispara SFX/FX/UI
                invulnFeedbackUntil = Time.time + invulnFeedbackCooldown; // trava feedback por um curto período
            }
            return false; // ignora o dano
        }

        var damageAmount = damage.Amount;

        CurrentHealth -= damageAmount;
        CurrentHealth = Math.Max(0, CurrentHealth);

        OnDamaged?.Invoke(damage);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        // 3) reativar i-frames (se a config assim definir)
        invulnUntil = Time.time + _invulnDuration;  // um único set resolve tudo

        if (CurrentHealth == 0)
        {
            Dead();
        }
        return true;
    }

    public void Dead()
    {
        if (_isDead) return;
        _isDead = true;
        invulnUntil = 0;
        OnDied?.Invoke();
    }

    void OnEnable()
    {
        if (_healthConfig == null)
        {
            Debug.LogError("GameObject sem HealthConfig");
            return;
        }

        MaxHealth = _healthConfig.MaxHealth;
        CurrentHealth = MaxHealth;

        _invulnDuration = _healthConfig.InvulnerabilityAfterDmg;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
