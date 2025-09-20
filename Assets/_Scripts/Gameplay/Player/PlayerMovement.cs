using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField, Min(0f)] float _speed = 5f;
    private Vector2 _move = new();

    private void OnEnable()
    {
        if (_rb == null)
            TryGetComponent<Rigidbody2D>(out _rb);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_rb == null) return;

        var moveNormalized = _move.normalized * _speed;
        _rb.MovePosition(_rb.position + moveNormalized * Time.fixedDeltaTime);
    }
}
