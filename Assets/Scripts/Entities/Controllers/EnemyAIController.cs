using UnityEngine;
using UnityEngine.Assertions.Must;


[RequireComponent(typeof(EntityMovement))]
public class EnemyAIController : MonoBehaviour
{
    public EntityMovement MovementComponent { get; private set; }
    public Animator Animator { get; private set;}
    public ObstacleDetector ObstaclesDetector { get; private set; }
    public PlayerDetector PlayerDetector { get; private set; }
    public IEntityOrientation Orientation { get; private set; }

    private EntityOrientation _orientationComponent;
    private Health _health;

    void Start()
    {
        MovementComponent = GetComponent<EntityMovement>();
        Animator = GetComponentInChildren<Animator>();
        ObstaclesDetector = GetComponent<ObstacleDetector>();
        PlayerDetector = GetComponent<PlayerDetector>();
        _orientationComponent = GetComponent<EntityOrientation>();
        Orientation = _orientationComponent;
        _health = GetComponent<Health>();
        
        if (_health != null)
        {
            _health.OnDeath += HandleDeath;
        }
    }

    public void SetMovementDirection(float directionX)
    {
        MovementComponent.SetDirection(directionX);
        
        if (_orientationComponent != null)
        {
            _orientationComponent.SetFacingDirection(directionX);
        }
    }

    private void HandleDeath()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null) rigidbody.linearVelocity = Vector2.zero;

        DamageOnTouch damageComponent = GetComponent<DamageOnTouch>();
        if (damageComponent != null) damageComponent.enabled = false;
        
        SetMovementDirection(0);
        this.enabled = false;

        int deadLayer = LayerMask.NameToLayer("DeadEnemy");
        if (deadLayer != -1)
        {
            gameObject.layer = deadLayer;
        }
    }

     private void OnDestroy()
    {
        if (_health != null)
        {
            _health.OnDeath -= HandleDeath;
        }
    }
}
