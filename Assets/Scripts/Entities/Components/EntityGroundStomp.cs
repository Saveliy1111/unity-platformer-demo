using UnityEngine;

public class EntityGroundStomp : MonoBehaviour
{
    [Header("Stomp Physics")]
    [SerializeField] private float _downwardVelocity = -20f;
    [SerializeField] private float _minHeightRequired = 3f;
    [SerializeField] private LayerMask _environmentLayer;

    [Header("Hit Detection")]
    [SerializeField] private int _damage = 2;
    [SerializeField] private Vector2 _hitBoxSize = new Vector2(3f, 1f);
    [SerializeField] private Vector2 _hitBoxOffset = new Vector2(0f, -0.5f);
    [SerializeField] private LayerMask _targetLayer;

    [Header("Cooldown")]
    [SerializeField] private float _cooldownDuration = 1f;

    [Header("Events (Visuals)")]
    public UnityEngine.Events.UnityEvent OnStompStartVisuals;
    public UnityEngine.Events.UnityEvent OnStompImpactVisuals;

    public bool IsStomping { get; private set; }

    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private EntityMovement _movement;
    private CountdownTimer _cooldownTimer = new CountdownTimer();

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
        _movement = GetComponent<EntityMovement>();
        
        _cooldownTimer.StartCountdown(0f);
    }
    

    void Update()
    {
        _cooldownTimer.Tick();
        
        if (IsStomping)
        {
            DamageEnemiesInPath();

            if (_groundDetector.isGrounded)
            {
                ExecuteImpact();
            }
        }
    }

    public void TryStomp()
    {
        if (IsStomping || _groundDetector.isGrounded) return;
        if (!_cooldownTimer.IsFinished) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _minHeightRequired, _environmentLayer);

        if (hit.collider == null) 
        {
            StartStompFalling();
        }
    }

    private void StartStompFalling()
    {
        IsStomping = true;
        _movement.LockMovement();

        _rigidbody.linearVelocity = new Vector2(0f, _downwardVelocity);
        OnStompStartVisuals?.Invoke();
    }

    private void ExecuteImpact()
    {
        IsStomping = false;
        _movement.UnlockMovement();

        DamageEnemiesInPath();
        _cooldownTimer.StartCountdown(_cooldownDuration);
        OnStompImpactVisuals?.Invoke();
    }

     private void DamageEnemiesInPath()
    {
        Vector2 boxCenter = (Vector2)transform.position + _hitBoxOffset;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(boxCenter, _hitBoxSize, 0f, _targetLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage, transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.down * _minHeightRequired);

        Gizmos.color = Color.red;
        Vector2 boxCenter = (Vector2)transform.position + _hitBoxOffset;
        Gizmos.DrawWireCube(boxCenter, _hitBoxSize);
    }
}
