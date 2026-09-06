using UnityEngine;

[RequireComponent(typeof(FlyingMovement))]
[RequireComponent(typeof(ObstacleDetector))]
public class FlyingNavigationStrategy : MonoBehaviour, IEnemyNavigationStrategy
{
    [Header("Patrol Settings")]
    [SerializeField] private float _turnCooldown = 0.3f;

    [Header("Glide Settings")]
    [SerializeField] private float _glideUpDirection = 1f;

    [Header("Chase Settings")]
    [SerializeField] private float _deadzoneY = 0.2f;

    public float CurrentDirection => _orientation != null ? _orientation.FacingDirection : -1f;
    public bool IsMoving => _movement != null && Mathf.Abs(_movement.DirectionX) > 0.01f;
    
    private IMovement _movement;
    private IOrientation _orientation;
    private ObstacleDetector _obstacleDetector;
    private CountdownTimer _turnCooldownTimer = new CountdownTimer();

    void Awake()
    {
        _movement = GetComponent<IMovement>();
        _orientation = GetComponent<IOrientation>();
        _obstacleDetector = GetComponent<ObstacleDetector>();
        
        _turnCooldownTimer.StartCountdown(0f);
    }

    void Update()
    {
        _turnCooldownTimer.Tick();
    }

    public float ExecutePatrol(float currentDirection, float speed)
    {
        _movement.SetSpeed(speed);
        
        if (!_turnCooldownTimer.IsFinished) return currentDirection;

        if (_obstacleDetector.IsHittingWall)
        {
            _turnCooldownTimer.StartCountdown(_turnCooldown);
            currentDirection *= -1f;
        }

        SetMovement(new Vector2(currentDirection, 0f));
        return currentDirection;
    }

    public void ExecuteChase(Transform target, float speed, float deadzoneX)
    {
        _movement.SetSpeed(speed);
        
        float directionX = GetDirectionToPlayer(target, deadzoneX);
        float directionY = GetVerticalDirectionToPlayer(target, _deadzoneY);

        if (_obstacleDetector.IsHittingWall)
        {
            directionY = _glideUpDirection;
        }

        SetMovement(new Vector2(directionX, directionY));
    }

    public void StopMovement()
    {
        SetMovement(Vector2.zero);
    }

    private void SetMovement(Vector2 direction)
    {
        _movement.SetDirection(direction);
        if (_orientation != null)
        {
            _orientation.SetFacingDirection(direction.x);
        }
    }

    private float GetDirectionToPlayer(Transform target, float deadzoneX)
    {
        if (target == null) return 0f;

        float distanceX = target.position.x - transform.position.x;
        
        if (Mathf.Abs(distanceX) < deadzoneX)
        {
            return _orientation != null ? _orientation.ForwardVector.x : Mathf.Sign(distanceX);
        }
        
        return Mathf.Sign(distanceX);
    }

    private float GetVerticalDirectionToPlayer(Transform target, float deadzoneY)
    {
        if (target == null) return 0f;

        float distanceY = target.position.y - transform.position.y;

        if (Mathf.Abs(distanceY) < deadzoneY)
        {
            return 0f;
        }

        return Mathf.Sign(distanceY);
    }
}