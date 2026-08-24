using UnityEngine;

public class EntityMovement : MonoBehaviour, IOnDeathListener
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 2f;

    private Rigidbody2D _rigidbody;
    private EntityStun _stunComponent;

    public float DirectionX { get; private set; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<EntityStun>();
    }

    public void SetDirection(float directionX)
    {
        DirectionX = directionX;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }


    private void FixedUpdate()
    {
        if (_stunComponent != null && _stunComponent.IsStunned)
        {
            return;
        }

        _rigidbody.linearVelocity 
        = new Vector2(DirectionX * _speed, _rigidbody.linearVelocity.y);
    }

    public void HandleDeath()
    {
        SetDirection(0);
        this.enabled = false;
    }
}
