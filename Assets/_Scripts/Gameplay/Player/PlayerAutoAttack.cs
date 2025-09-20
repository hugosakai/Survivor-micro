using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour, IAttackDriver
{
    [SerializeField] DamageDealer _dealer;
    [SerializeField] AttackConfig _attackConfig;
    Coroutine _loop;

    void OnEnable()
    {
        StartFiring();
    }
    void OnDisable()
    {
        if (_loop != null) StopFiring();
    }

    private IEnumerator FireLoop()
    {
        yield return new WaitForSeconds(_attackConfig.initialDelay); // opcional: atraso inicial
        while (true)
        {
            _dealer.OpenWindow(_attackConfig.window); // dispara a janela
            yield return new WaitForSeconds(_attackConfig.window);
            yield return new WaitForSeconds(_attackConfig.interval);   // sempre garanta window <= interval
        }
    }

    public void StartFiring()
    {
        _loop = StartCoroutine(FireLoop());
    }

    public void StopFiring()
    {
        StopCoroutine(_loop);
    }

    public void SetAttackConfig(AttackConfig attack)
    {
        _attackConfig = attack;
    }
}
