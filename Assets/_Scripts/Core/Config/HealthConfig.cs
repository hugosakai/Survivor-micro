// _Scripts/Core/Config/HealthConfig.cs
using UnityEngine;

[CreateAssetMenu(fileName ="Health", menuName ="Health")]
public class HealthConfig : ScriptableObject
{
    [Tooltip("Vida máxima")]
    [Min(0.0f)][SerializeField] private float MaxHealth;
    [Tooltip("Recuperação da vida / segundo")]
    [Min(0.0f)][SerializeField] private float RegenPerSecond;
    [Tooltip("Invunerabilidade após tomar Dano")]
    [Min(0.0f)][SerializeField] private float InvulnerabilityAfterDmg;
}
