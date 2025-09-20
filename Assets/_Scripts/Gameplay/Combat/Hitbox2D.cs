//_Scripts/Gameplay/Combat/Hitbox2D.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox2D : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] Collider2D _col;          // seu trigger

    private ContactFilter2D _filter = new ContactFilter2D();
    readonly List<Collider2D> _buffer = new List<Collider2D>(16);


    public event Action<IHealth, GameObject, Collider2D> OnHitCandidate;    //Vida, objeto acertado, colisor do objeto
    public bool Active { get; private set; }

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        if (_col == null) Debug.LogError("Hitbox2D sem Collider2D");
        if(!_col.isTrigger) Debug.LogWarning("Collider não é trigger");

        _filter.useLayerMask = true;
        _filter.SetLayerMask(_targetMask);
        _filter.useTriggers = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Active) return;
        if (((1 << collider.gameObject.layer) & _targetMask.value) == 0) return;
        var target = collider.gameObject.GetComponentInParent<IHealth>();
        if (target != null)
        {
            Debug.Log("trigger enter 2d");
            
            OnHitCandidate?.Invoke(target, collider.gameObject, collider);
        }
    }

    public void SetActive(bool v)
    {
        Active = v;
        if (Active) PrimeExistingOverlaps(); // varre e emite candidatos já sobrepostos
    }

    private void PrimeExistingOverlaps()
    {
        if (!Active || !_col) return;

        _buffer.Clear();
        _col.OverlapCollider(_filter, _buffer); // coleta quem já está dentro

        // (opcional) se quiser evitar duplicatas de um mesmo inimigo com múltiplos colliders:
        // var seen = new HashSet<IHealth>();

        foreach (var c in _buffer)
        {
            // checar layer cedo (barato)
            if (((1 << c.gameObject.layer) & _targetMask.value) == 0) continue;

            var h = c.GetComponentInParent<IHealth>(); // Health pode estar no root
            if (h == null) continue;

            // if (seen.Contains(h)) continue; else seen.Add(h);

            OnHitCandidate?.Invoke(h, c.gameObject, c); // emite os candidatos já sobrepostos
        }
    }
}
