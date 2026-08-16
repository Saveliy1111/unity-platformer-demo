using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class PlayerController : MonoBehaviour
{
    private EntityMovement _movementComponent;
    private EntityJump _jumpComponent;

    void Start()
    {
        _movementComponent = GetComponent<EntityMovement>();
        _jumpComponent = GetComponent<EntityJump>();
    }

    void Update()
    {
        PerformMovement();
        PerformJump();
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("You died!");
        _movementComponent.SetDirection(0);
        this.enabled = false;
    }

    private void PerformMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        _movementComponent.SetDirection(moveInput);
    }

    private void PerformJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpComponent.Jump();
        }
    }
}
