using System.Collections.Generic;
using UnityEngine;

public class PhysicsTickScheduler : MonoBehaviour
{
    private readonly List<IPhysicsTickable> _tickables = new();
    private readonly List<IPhysicsTickable> _toAdd = new();
    private readonly List<IPhysicsTickable> _toRemove = new();
    public static PhysicsTickScheduler Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void FixedUpdate() {
        float dt = Time.fixedDeltaTime;

        // aplica pendências antes
        if (_toAdd.Count > 0)
        {
            foreach (IPhysicsTickable tick in _toAdd)
            {
                _tickables.Add(tick);
            }
            _toAdd.Clear();
        }
        if (_toRemove.Count > 0)
        {
            foreach (IPhysicsTickable tick in _toRemove)
            {
                _tickables.Remove(tick);
            }
            _toRemove.Clear();
        }

        // Limpar nulls (loop invertido)
        for (int i = _tickables.Count - 1; i >= 0; i--)
            if (_tickables[i] == null) _tickables.RemoveAt(i);

        for (int i = 0; i < _tickables.Count; i++)
            _tickables[i].PhysicsTick(dt);
    }

    public void Register(IPhysicsTickable t)
    {
        if (t == null) return;
        if (_tickables.Contains(t) || _toAdd.Contains(t)) return; // idempotente
        // se estava para remover, cancela a remoção
        int idx = _toRemove.IndexOf(t);
        if (idx >= 0) { _toRemove.RemoveAt(idx); return; }
        _toAdd.Add(t);
    }

    public void Unregister(IPhysicsTickable t)
    {
        if (t == null) return;
        // se está pendente de add, cancela o add
        int idx = _toAdd.IndexOf(t);
        if (idx >= 0) { _toAdd.RemoveAt(idx); return; }
        if (_tickables.Contains(t) && !_toRemove.Contains(t))
            _toRemove.Add(t);
    }
}
