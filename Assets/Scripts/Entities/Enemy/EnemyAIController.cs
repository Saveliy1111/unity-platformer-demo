using UnityEngine;
using UnityEngine.Assertions.Must;


[RequireComponent(typeof(Movement))]
public class EnemyAIController : MonoBehaviour
{
    private Orientation _orientationComponent;
    private Health _health;

    public Movement MovementComponent { get; private set; }
    public Animator Animator { get; private set;}
    public ObstacleDetector ObstaclesDetector { get; private set; }
    public PlayerDetector PlayerDetector { get; private set; }
    public IOrientation Orientation { get; private set; }

    void Start()
    {
        MovementComponent = GetComponent<Movement>();
        Animator = GetComponentInChildren<Animator>();
        ObstaclesDetector = GetComponent<ObstacleDetector>();
        PlayerDetector = GetComponent<PlayerDetector>();
        _orientationComponent = GetComponent<Orientation>();
        Orientation = _orientationComponent;
        _health = GetComponent<Health>();
        
        if (_health != null)
        {
            _health.OnDeath += HandleDeath;
            _health.OnTakeDamage += HandleTakeDamage;
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

    private void HandleTakeDamage(Transform attacker)
    {
        if (PlayerDetector != null && attacker != null)
        {
            PlayerDetector.ForceAggro(attacker);
        }
    }

    private void HandleDeath()
    {
        FreezePhysics();
        
        IOnDeathListener[] deathListeners = GetComponents<IOnDeathListener>();
        foreach (var listener in deathListeners)
        {
            listener.HandleDeath();
        }

        SwitchToDeadLayer();
        this.enabled = false;
    }

    private void FreezePhysics()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.linearVelocity = new Vector2(0f, rigidbody.linearVelocity.y);
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void SwitchToDeadLayer()
    {
        int deadLayer = LayerMask.NameToLayer("DeadEnemies");
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
            _health.OnTakeDamage -= HandleTakeDamage;
        }
    }
}
