using UnityEngine;

public class FlyingMovement : MonoBehaviour, IMovement, IOnDeathListener
{
    [Header("Flying Movement Settings")]
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _acceleration = 20f;
    [SerializeField] private float _deceleration = 30f;

    private Vector2 _direction;
    public float DirectionX => _direction.x;

    private Rigidbody2D _rigidbody;
    private Stun _stunComponent;
    private bool _isLocked = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<Stun>();
        
        if (_rigidbody != null)
        {
            _rigidbody.gravityScale = 0f;
        }
    }

    public void SetDirection(float directionX)
    {
        _direction = new Vector2(directionX, _direction.y);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SetSpeed(float newSpeed)
    {
        _maxSpeed = newSpeed;
    }

    void FixedUpdate()
    {
        if (_stunComponent != null && _stunComponent.IsStunned) return;
        if (_isLocked) return;

        float newVelocityX = CalculateVelocity1D(_direction.x, _rigidbody.linearVelocity.x);
        float newVelocityY = CalculateVelocity1D(_direction.y, _rigidbody.linearVelocity.y);

        _rigidbody.linearVelocity = new Vector2(newVelocityX, newVelocityY);
    }

    public void LockMovement()
    {
        _isLocked = true;
        SetDirection(Vector2.zero);
    }

    public void UnlockMovement()
    {
        _isLocked = false;
    }

    public void HandleDeath()
    {
        LockMovement();
        if (_rigidbody != null)
        {
            _rigidbody.gravityScale = 1f;
        }
        this.enabled = false;
    }

    private float CalculateVelocity1D(float inputDirection, float currentVelocity)
    {
        float targetSpeed = inputDirection * _maxSpeed;
        float currentAccel = (Mathf.Abs(inputDirection) > 0.01f) ? _acceleration : _deceleration;
        return Mathf.MoveTowards(currentVelocity, targetSpeed, currentAccel * Time.fixedDeltaTime);
    }
}