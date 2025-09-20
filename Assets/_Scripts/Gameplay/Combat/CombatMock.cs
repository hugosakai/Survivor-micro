// _Scripts/Gameplay/Combat/CombatMock.cs
using System;
using System.Collections;
using UnityEngine;

public class CombatMock : MonoBehaviour
{
    [SerializeField] PlayerAttackLoadout _loadout;
    [SerializeField] AttackDefinition[] _def;
    public void Give()
    {
        foreach (AttackDefinition atk in _def)
        {
            _loadout.AddAttack(atk);
        }
    } // chamar por botão/trigger

    public void Start()
    {
        Give();
    }
}
