using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityMovement))]
public class PlayerController : MonoBehaviour
{
    private EntityMovement _movementComponent;
    private EntityJump _jumpComponent;
    private EntityOrientation _orientation;
    private Health _health;

    private PlayerInputActions _inputActions; 

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
        }
        
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
    }

    void Start()
    {
        _movementComponent = GetComponent<EntityMovement>();
        _jumpComponent = GetComponent<EntityJump>();
        _orientation = GetComponent<EntityOrientation>();
        _health = GetComponent<Health>();

        if (_health != null)
        {
            _health.OnDeath += HandleDeath; 
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        float moveInput = context.ReadValue<float>(); 
        _movementComponent.SetDirection(moveInput);

        if (_orientation != null)
        {
            _orientation.SetFacingDirection(moveInput);
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movementComponent.SetDirection(0f);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (_jumpComponent != null)
        {
            _jumpComponent.Jump();
        }
    }

    private void HandleDeath()
    {
        _inputActions.Disable();
        this.enabled = false;
    }
    
    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.OnDeath -= HandleDeath; 
        }
    }
}