using System.Collections;
using UnityEngine;

public class EnemyContactAttack : MonoBehaviour, IAttackDriver
{
    [SerializeField] DamageDealer _dealer;
    [SerializeField] AttackConfig _attackConfig;
    private Coroutine _loop;
    private void OnEnable() // inicia loop de pulsos: dealer.OpenWindow(_window) a cada _pulseEvery
    {
        if (_dealer == null) TryGetComponent(out _dealer);  // fallback
        if (_dealer == null) { enabled = false; return; }   // fail-safe
        StartFiring();
    }
    private void OnDisable() // para loop
    {
        if (_loop != null) StopFiring();
    }

    private IEnumerator PulseLoop()
    {
        yield return new WaitForSeconds(_attackConfig.initialDelay);
        while (true)
        {
            _dealer.OpenWindow(_attackConfig.window);
            yield return new WaitForSeconds(_attackConfig.window);
            yield return new WaitForSeconds(_attackConfig.interval);
        }

    }

    public void StartFiring()
    {
        _loop = StartCoroutine(PulseLoop());
    }

    public void StopFiring()
    {
        StopCoroutine(_loop);
    }
}
