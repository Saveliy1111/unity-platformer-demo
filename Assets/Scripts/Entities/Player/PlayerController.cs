using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
public class PlayerController : MonoBehaviour, IPlayerActiveListener
{
    private Movement _movement;
    private Jump _jump;
    private Health _health;
    private Orientation _orientation;
    private bool _isActive = false;


    void Awake()
    {
        _movement = GetComponent<Movement>();
        _jump = GetComponent<Jump>();
        _health = GetComponent<Health>();
        _orientation = GetComponent<Orientation>();
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

        if (_orientation != null)
        {
            _orientation.SetFacingDirection(direction);
        }
    }

    private void HandleMoveCanceled()
    {
        if (!_isActive || IsDead()) return;
        _movement.SetDirection(0f);
    }

    private void HandleJump()
    {
        if (!_isActive || IsDead()) return;
        _jump.PerformJump();
    }

    private void HandleJumpCanceled()
    {
        if (!_isActive || IsDead()) return;
        _jump.CancelJump(); 
    }
}
