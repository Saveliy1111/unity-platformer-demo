using UnityEngine;
using UnityEngine.Pool;

public class LaserProjectile : Projectile
{
    [Header("Laser Projectile Settings")]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private int _damage = 1;

    private float _currentLifetime;
    private TrailRenderer _trailRenderer;

    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        _currentLifetime = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        
        _currentLifetime += Time.deltaTime;
        if (_currentLifetime >= _lifetime)
        {
            ReturnToPool();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int colLayer = collider.gameObject.layer;
        if (collider.gameObject.CompareTag(Constants.PLAYER_TAG) 
        || colLayer == LayerMask.NameToLayer("Pinkus") 
        || colLayer == LayerMask.NameToLayer("Green'kor")
        || colLayer == LayerMask.NameToLayer("Green'korHead"))
        {
            return;
        }

        if (collider.gameObject.CompareTag(Constants.ENEMY_TAG) && collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage, _shooterTransform);
        }

        ReturnToPool();
    }

    public override void Launch()
    {
        base.Launch();
        
        if (_trailRenderer != null) 
        {
            _trailRenderer.Clear();
        }
    }
}
