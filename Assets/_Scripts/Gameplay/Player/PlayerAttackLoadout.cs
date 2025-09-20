using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLoadout : MonoBehaviour
{
    [SerializeField] Transform _attachParent;        // onde os ataques ficam (ex.: um empty "Attacks")
    [SerializeField] int _teamId = 0;                // time do player
    [SerializeField] TeamRelationsSO _relations;     // o mesmo usado no Dealer

    // API pública:
    public void AddAttack(AttackDefinition def)
    {
        var attack = Instantiate(def.attackPrefab, _attachParent);
        var dealer = attack.GetComponentInChildren<DamageDealer>();
        var driver = attack.GetComponentInChildren<PlayerAutoAttack>();
        dealer.SetRoot(_attachParent.root);
        dealer.SetTeamId(_teamId);
        dealer.SetTeamRelations(_relations);
        driver.SetAttackConfig(def.attackConfig);
    }
    // public bool HasAttack(AttackDefinition def)
    // {
    //     /* opcional: por id */
    // }
    // opcional nível:
    // public void UpgradeAttack(AttackDefinition def) { /* aumenta nível */ }
}
