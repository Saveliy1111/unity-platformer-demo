using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class PlayerController : MonoBehaviour
{
    private EntityMovement _movementComponent;
    private EntityJump _jumpComponent;
    private EntityOrientation _orientation;
    private Health _health;

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

    void Update()
    {
        PerformMovement();
        PerformJump();
    }

    private void PerformMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        _movementComponent.SetDirection(moveInput);

        if (_orientation != null)
        {
            _orientation.SetFacingDirection(moveInput);
        }
    }

    private void PerformJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpComponent.Jump();
        }
    }

    private void HandleDeath()
    {
        _movementComponent.SetDirection(0);
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
