//_Scripts/Gameplay/Combat/DamageDealer.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageSource, IOwnerProvider
{
    [SerializeField, Min(0f)] private float _amount;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private DamageSource _damageSource;

    [Header("Anti Auto-Hit / Team-Hit")]
    [SerializeField] Transform _ownerRoot;
    [SerializeField] int _teamId = 0;
    [SerializeField] bool _friendlyFire = false;
    [SerializeField] private TeamRelationsSO _teamRelationsAsset;

    [Header("Cap de alvos")]
    [SerializeField] int _maxTargets = -1;      //-1 sem limite
    private Hitbox2D _hitbox2D;
    bool _windowOpen;
    HashSet<IHealth> _hitThisWindow = new();

    public float Amount => _amount;

    public DamageType Type => _damageType;

    public DamageSource Source => _damageSource;

    public Transform OwnerRoot => _ownerRoot;

    public event Action<IHealth, Vector3> OnDealt;
    public event Action<IHealth, Vector3> OnBlocked;

    private void Awake()
    {
        _hitbox2D = GetComponent<Hitbox2D>();
        if (_hitbox2D == null) Debug.LogError("Hitbox2D não encontrado.");
        if (_teamRelationsAsset == null) Debug.LogError("TeamRelationsSO não encontrado");

        if (_ownerRoot == null)
        {
            _ownerRoot = transform.root;
        }
    }

    private void OnEnable()
    {
        if (_hitbox2D == null) return;
        _hitbox2D.OnHitCandidate += HitTarget;
    }

    private void HitTarget(IHealth target, GameObject targetGO, Collider2D collider)
    {
        if (!_windowOpen) return;              // gate do dealer

        if (target == null || targetGO == null) return;

        var targetRoot = targetGO.transform.root;
        if (targetRoot == _ownerRoot) return;   //se o alvo é ele próprio

        if (_maxTargets >= 0 && _hitThisWindow.Count >= _maxTargets) return;

        if (_hitThisWindow.Contains(target)) return;  // 1x por alvo nesta janela

        int targetTeam;
        bool hasTeam = TryGetTeam(targetGO, out targetTeam);
        if (_teamRelationsAsset != null)
        {
            if (hasTeam && !_friendlyFire && _teamRelationsAsset.IsFriendly(_teamId, targetTeam)) return;      //se o alvo é do mesmo time
        }
        else
        {
            if (hasTeam && !_friendlyFire && IsFriendly(_teamId, targetTeam)) return;      //se o alvo é do mesmo time
        }

        var damage = new Damage(_amount, _damageType, _damageSource);
        var hitEnemy = target.TakeDamage(damage);

        if (hitEnemy)
        {
            Vector3 hitPos;
            if (collider != null)
            {
                Vector2 p = collider.ClosestPoint((Vector2)transform.position);
                hitPos = new Vector3(p.x, p.y, targetGO.transform.position.z);
            }
            else
            {
                hitPos = targetGO.transform.position;
            }
            OnDealt?.Invoke(target, hitPos);
        }
        else
        {
            OnBlocked?.Invoke(target, targetGO.transform.position);
        }

        _hitThisWindow.Add(target); //adiciona alvo na lista para impedir que ele seja alvo mais de 1x.


    }

    private bool TryGetTeam(GameObject target, out int targetTeam)
    {
        targetTeam = target.GetComponentInParent<ITeamProvider>()?.TeamId ?? -1;
        if (targetTeam == -1)
        {
            return false;
        }
        return true;
    }
    private void OnDisable()
    {
        if (_hitbox2D) _hitbox2D.OnHitCandidate -= HitTarget;
    }

    public void OpenWindow()
    {
        _hitThisWindow.Clear();
        _windowOpen = true;
        
        if (_hitbox2D)
            _hitbox2D.SetActive(true);
    }

    public void CloseWindow()
    {
        _windowOpen = false;
        _hitThisWindow.Clear();
        
        if (_hitbox2D)
            _hitbox2D.SetActive(false);
    }


    public bool IsFriendly(int a, int b)
    {
        if (a == b)
        {
            return true;
        }
        return false;
    }

    public void OpenWindow(float seconds)
    {
        _hitThisWindow.Clear();
        _windowOpen = true;
        _hitbox2D.SetActive(true);
        StartCoroutine(StartAfterSeconds(seconds, () => CloseWindow()));
         }

    private IEnumerator StartAfterSeconds(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

}
