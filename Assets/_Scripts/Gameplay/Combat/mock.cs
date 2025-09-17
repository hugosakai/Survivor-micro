using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mock : MonoBehaviour, IHealth, IDamageDealer
{
    public int CurrentHealth => throw new System.NotImplementedException();

    public int MaxHealth => throw new System.NotImplementedException();

    public void Damage()
    {
        throw new System.NotImplementedException();
    }

    public void Heal()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
