using UnityEngine;

public class EnemyChase : MonoBehaviour, IPhysicsTickable
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _target;  // Player
    [SerializeField] float _speed = 3f;
    [SerializeField] float _stopDistance = 0.8f;
    private bool _canMove = true;

    private void OnEnable()
    {
        if (_rb == null)
            TryGetComponent<Rigidbody2D>(out _rb);

        if (PhysicsTickScheduler.Instance != null) PhysicsTickScheduler.Instance.Register(this);
    }
    private void Start()
    {
        if (_target == null)
        {
            var target = GameObject.FindGameObjectWithTag("Player");
            SetTarget(target.transform);
        }
    }

    public void SetTarget(Transform t)
    {
        _target = t;
    }

    public void PhysicsTick(float fixedDeltaTime)
    {
        if (_target == null) return;

        float distance = Vector2.Distance(_target.position, _rb.position);
        _canMove = distance >= _stopDistance;

        if (!_canMove) return;

        var normalizedSpeed = ((Vector2)_target.position - _rb.position).normalized * _speed;
        _rb.MovePosition(_rb.position + normalizedSpeed * fixedDeltaTime);
    }

    void OnDisable()
    {
        if(PhysicsTickScheduler.Instance != null) PhysicsTickScheduler.Instance.Unregister(this);
    }
}
