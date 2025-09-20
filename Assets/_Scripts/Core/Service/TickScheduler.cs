using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickScheduler : MonoBehaviour
{
    private readonly List<ITickable> _tickables = new();
    private readonly List<ITickable> _toAdd = new();
    private readonly List<ITickable> _toRemove = new();
    public static TickScheduler Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update() {
        float dt = Time.deltaTime;

        // aplica pendências antes
        if (_toAdd.Count > 0)
        {
            foreach (ITickable tick in _toAdd)
            {
                _tickables.Add(tick);
            }
            _toAdd.Clear();
        }
        if (_toRemove.Count > 0)
        {
            foreach (ITickable tick in _toRemove)
            {
                _tickables.Remove(tick);
            }
            _toRemove.Clear();
        }
        for (int i = _tickables.Count - 1; i >= 0; i--)
            if (_tickables[i] == null) _tickables.RemoveAt(i);
        

        for (int i = 0; i < _tickables.Count; i++)
            _tickables[i].Tick(dt);
    }

    public void Register(ITickable t)
    {
        if (t == null) return;
        if (_tickables.Contains(t) || _toAdd.Contains(t)) return; // idempotente
        _toAdd.Add(t);
    }

    public void Unregister(ITickable t)
    {
        if (t == null) return;
        // Se já está em _toAdd, cancela; se está em _tickables, agenda remoção
        _toRemove.Add(t);
    }
}
