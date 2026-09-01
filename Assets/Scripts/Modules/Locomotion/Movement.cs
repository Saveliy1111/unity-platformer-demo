using UnityEngine;

public class Movement : MonoBehaviour, IOnDeathListener
{
    [Header("Movement Settings")]
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _acceleration = 20f;
    [SerializeField] private float _deceleration = 30f;

    public float DirectionX { get; private set; }

    private Rigidbody2D _rigidbody;
    private Stun _stunComponent;
    private bool _isLocked = false;

    public void SetDirection(float directionX)
    {
        DirectionX = directionX;
    }

    public void SetSpeed(float newSpeed)
    {
        _maxSpeed = newSpeed;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<Stun>();
    }

    void FixedUpdate()
    {
        if (_stunComponent != null && _stunComponent.IsStunned) return;
        if (_isLocked) return;
        

        float newVelocityX = CalculateVelocityX();
        _rigidbody.linearVelocity = new Vector2(newVelocityX, _rigidbody.linearVelocity.y);
    }

    public void LockMovement()
    {
        _isLocked = true;
        SetDirection(0f);
    }

    public void UnlockMovement()
    {
        _isLocked = false;
    }

    public void HandleDeath()
    {
        LockMovement();
        this.enabled = false;
    }

    private float CalculateVelocityX()
    {
        float targetSpeed = DirectionX * _maxSpeed;
        float currentAccel = (Mathf.Abs(DirectionX) > 0.01f) ? _acceleration : _deceleration;
        float newVelocityX = Mathf.MoveTowards(_rigidbody.linearVelocity.x, targetSpeed, currentAccel * Time.fixedDeltaTime);

        return newVelocityX;
    }
}
