using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public PlayerDetector PlayerDetector { get; private set; }
    public IEnemyNavigationStrategy NavigationStrategy { get; private set; }

    private Health _health;

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        PlayerDetector = GetComponent<PlayerDetector>();
        NavigationStrategy = GetComponent<IEnemyNavigationStrategy>();
        
        _health = GetComponent<Health>();
        if (_health != null)
        {
            _health.OnDeath += HandleDeath;
            _health.OnTakeDamage += HandleTakeDamage;
        }
    }

    private void HandleTakeDamage(int damage, Transform attacker)
    {
        if (PlayerDetector != null && attacker != null && damage > 0)
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