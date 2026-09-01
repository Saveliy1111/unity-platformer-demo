using UnityEngine;
using UnityEngine.Events;

public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private float _coyoteTimeDuration = 0.15f;
    [SerializeField] private float _jumpBufferDuration = 0.1f;
    [SerializeField] private float _jumpCooldownDuration = 0.1f;
    [SerializeField] private float _jumpCutMultiplier = 0.5f;

    [Header("Events (For UI & Visuals)")]
    public UnityEvent OnJumpVisuals;

    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private CountdownTimer _coyoteTimer = new CountdownTimer();
    private CountdownTimer _bufferTimer = new CountdownTimer();
    private CountdownTimer _jumpCooldownTimer = new CountdownTimer();
    private bool _canJump;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();

        _bufferTimer.StartCountdown(0f);
        _jumpCooldownTimer.StartCountdown(0f);
    }

    void Update()
    {
        _coyoteTimer.Tick();
        _bufferTimer.Tick();
        _jumpCooldownTimer.Tick();

        if (_groundDetector.isGrounded && _jumpCooldownTimer.IsFinished)
        {
            _coyoteTimer.StartCountdown(_coyoteTimeDuration);
            _canJump = true;
        }
        else
        {
            if (_coyoteTimer.IsFinished)
            {
                _canJump = false; 
            }
        }

        
        if (!_bufferTimer.IsFinished && _canJump)
        {
            ExecuteJump();
        }
    }

    public void PerformJump()
    {
        _bufferTimer.StartCountdown(_jumpBufferDuration);
    }

    public void CancelJump()
{
    if (_rigidbody.linearVelocity.y > 0)
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * _jumpCutMultiplier);
    }
}

    private void ExecuteJump()
    {
        StopLinearVelocityOnCoyoteJump();
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

        OnJumpVisuals?.Invoke();

        _coyoteTimer.StartCountdown(0f);
        _bufferTimer.StartCountdown(0f);
        _canJump = false;

        _jumpCooldownTimer.StartCountdown(_jumpCooldownDuration);
    }

    private void StopLinearVelocityOnCoyoteJump()
    {
        if (_rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        }
    }
}
