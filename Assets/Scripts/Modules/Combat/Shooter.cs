using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [Header("Weapon Settings")]
    [Tooltip("Projectile Prefab")]
    [SerializeField] private Projectile _projectilePrefab;
    [Tooltip("Shooting Point")]
    [SerializeField] private Transform _firePoint;
    [Tooltip("Time needed to charge the shot")]
    [SerializeField] private float _chargeTime = 0.5f;
    [SerializeField] private float _shootCooldown = 0.83f;

    [Header("Events")]
    public UnityEvent OnChargeVisuals;
    public UnityEvent OnShootVisuals;

    private ObjectPool<Projectile> _pool;
    private CountdownTimer _chargeTimer = new CountdownTimer();
    private CountdownTimer _shootCooldownTimer = new CountdownTimer();

    private bool _isCharging;
    private Vector3 _currentTarget;

    void Start()
    {
        _pool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnToPool,
            actionOnDestroy: OnDestroyProjectile,
            defaultCapacity: 5,
            maxSize: 15
        );
    }

    void Update()
    {
        _shootCooldownTimer.Tick();
        if (_isCharging)
        {
            _chargeTimer.Tick();
            if (_chargeTimer.IsFinished)
            {
                ExecuteShoot();
            }
        }
    }

    public void StartShooting(Vector3 targetWorldPosition)
    {
        if (_isCharging) return;
        if (!_shootCooldownTimer.IsFinished) return;

        _currentTarget = targetWorldPosition;
        _isCharging = true;
        _chargeTimer.StartCountdown(_chargeTime);

        OnChargeVisuals?.Invoke();
    }

    private void ExecuteShoot()
    {
        _isCharging = false;

        _shootCooldownTimer.StartCountdown(_shootCooldown);

        Projectile projectile = _pool.Get();
        projectile.transform.position = _firePoint.position;

        Vector3 direction = (_currentTarget - _firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        projectile.Launch();

        OnShootVisuals?.Invoke();
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(_projectilePrefab);
        projectile.Setup(_pool, transform);
        return projectile;
    }

    private void OnTakeFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
