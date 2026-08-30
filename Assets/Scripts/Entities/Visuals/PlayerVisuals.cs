using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private EntityStun _stunComponent;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _groundDetector = GetComponentInParent<GroundDetector>();
        _stunComponent = GetComponentInParent<EntityStun>();
    }

    void Update()
    {
        if (_stunComponent != null && _stunComponent.IsStunned) return;

        UpdateAnimationParameters();
    }

    private void UpdateAnimationParameters()
    {
        UpdateIsWalking();
        UpdateIsJumping();
        UpdateIsFalling();
    }

    private void UpdateIsWalking()
    {
        bool isWalking = Mathf.Abs(_rigidbody.linearVelocity.x) > 0.1f;
        _animator.SetBool("isWalking", isWalking);
    }

    private void UpdateIsJumping()
    {
        bool isJumping = !_groundDetector.isGrounded && _rigidbody.linearVelocity.y > 0f;
        _animator.SetBool("isJumping", isJumping);
    }

    private void UpdateIsFalling()
    {
        bool isFalling = !_groundDetector.isGrounded && _rigidbody.linearVelocity.y < 0f;
        _animator.SetBool("isFalling", isFalling);
    }

    public void TriggerDeathVisuals()
    {
        _animator.SetBool("isDead", true);
        
        _animator.SetTrigger("die");

        this.enabled = false; 
    }
}
