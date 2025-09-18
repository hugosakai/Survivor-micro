// _Scripts/Core/Config/HealthConfig.cs
using UnityEngine;

[CreateAssetMenu(fileName ="Health", menuName ="Health")]
public class HealthConfig : ScriptableObject
{
    [Tooltip("Vida máxima")]
    [Min(0.0f)][SerializeField] private float _maxHealth;
    [Tooltip("Recuperação da vida / segundo")]
    [Min(0.0f)][SerializeField] private float _regenPerSecond;
    [Tooltip("Invunerabilidade após tomar Dano")]
    [Min(0.0f)][SerializeField] private float _invulnerabilityAfterDmg;

    public float MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
    public float RegenPerSecond { get => _regenPerSecond; private set => _regenPerSecond = value; }
    public float InvulnerabilityAfterDmg { get => _invulnerabilityAfterDmg; private set => _invulnerabilityAfterDmg = value; }
}
