using UnityEngine;

[CreateAssetMenu(menuName="Config/Attack Definition")]
public sealed class AttackDefinition : ScriptableObject 
{
    public GameObject attackPrefab;   // prefab com Dealer+Hitbox+AutoAttack
    public AttackConfig attackConfig;
    public string id;
    public int maxLevel = 1;
    public bool allowDuplicates = false;
}
