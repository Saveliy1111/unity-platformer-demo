using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    void Update()
    {
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
}
