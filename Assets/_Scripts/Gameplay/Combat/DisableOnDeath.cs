using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] Transform _attacksRoot;
    [SerializeField] MonoBehaviour[] _toDisable;   // EnemyChase, EnemyContactAttack, PlayerMovement, PlayerAutoAttack, etc.
    [SerializeField] Collider2D[] _toDisableCols;  // hitboxes/hurtboxes se preciso
    [SerializeField] DamageDealer[] _dealers;      // opcional: fechar janelas ativas

    void OnEnable()
    {
        _health.OnDied += Handle;
    }
    void OnDisable()
    {
        _health.OnDied -= Handle;
    }

    void Handle()
    {
        foreach (MonoBehaviour behaviour in _toDisable)
        {
            behaviour.enabled = false;
        }

        foreach (Collider2D col in _toDisableCols)
        {
            col.enabled = false;
        }

        foreach (DamageDealer damageDealer in _dealers)
        {
            damageDealer.CloseWindow();
        }
        if(_attacksRoot != null)
            _attacksRoot.gameObject.SetActive(false);
    }
}
