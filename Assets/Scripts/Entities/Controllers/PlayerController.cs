using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityJump))]
public class PlayerController : MonoBehaviour, IPlayerActiveListener
{
    private EntityMovement _movement;
    private EntityJump _jump;
    private Health _health;
    private bool _isActive = false;


    void Awake()
    {
        _movement = GetComponent<EntityMovement>();
        _jump = GetComponent<EntityJump>();
        _health = GetComponent<Health>();
    }

    void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnMove += HandleMove;
            InputManager.Instance.OnMoveCanceled += HandleMoveCanceled;
            InputManager.Instance.OnJump += HandleJump;
            InputManager.Instance.OnJumpCanceled += HandleJumpCanceled;
        }
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnMove -= HandleMove;
            InputManager.Instance.OnMoveCanceled -= HandleMoveCanceled;
            InputManager.Instance.OnJump -= HandleJump;
            InputManager.Instance.OnJumpCanceled -= HandleJumpCanceled;
        }
    }

    public void OnActiveStateChanged(bool isActive)
    {
        _isActive = isActive;

        if (!_isActive && _movement != null)
        {
            _movement.SetDirection(0f);
        }
    }

    private bool IsDead()
    {
        return _health != null && _health.IsDead;
    }

    private void HandleMove(float direction)
    {
        if (!_isActive || IsDead()) return;
        _movement.SetDirection(direction);
    }

    private void HandleMoveCanceled()
    {
        if (!_isActive || IsDead()) return;
        _movement.SetDirection(0f);
    }

    private void HandleJump()
    {
        if (!_isActive || IsDead()) return;
        _jump.Jump();
    }

    private void HandleJumpCanceled()
    {
        if (!_isActive || IsDead()) return;
        _jump.CancelJump(); 
    }
}
