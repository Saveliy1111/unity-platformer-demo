using UnityEngine;

[RequireComponent(typeof(GroundMovement))]
[RequireComponent(typeof(ObstacleDetector))]
[RequireComponent(typeof(Jump))]
public class GroundNavigationStrategy : MonoBehaviour, IEnemyNavigationStrategy
{
    [Header("Patrol Settings")]
    [SerializeField] private float _turnCooldown = 0.3f;
    
    [Header("Chase Settings")]
    [SerializeField] private RandomTimer _jumpCooldownTimer;

    public float CurrentDirection => _orientation != null ? _orientation.FacingDirection : -1f;
    public bool IsMoving => _movement != null && Mathf.Abs(_movement.DirectionX) > 0.01f;

    private IMovement _movement;
    private IOrientation _orientation;
    private ObstacleDetector _obstacleDetector;
    private Jump _jump;
    private CountdownTimer _turnCooldownTimer = new CountdownTimer();

    void Awake()
    {
        _movement = GetComponent<IMovement>();
        _orientation = GetComponent<IOrientation>();
        _obstacleDetector = GetComponent<ObstacleDetector>();
        _jump = GetComponent<Jump>();
        
        _turnCooldownTimer.StartCountdown(0f);
    }

    void Update()
    {
        _turnCooldownTimer.Tick();
    }

    public float ExecutePatrol(float currentDirection, float patrolSpeed)
    {
        _movement.SetSpeed(patrolSpeed);
        
        if (!_turnCooldownTimer.IsFinished) return currentDirection;

        if (_obstacleDetector.IsHittingWall || _obstacleDetector.IsLedge)
        {
            _turnCooldownTimer.StartCountdown(_turnCooldown);
            currentDirection *= -1f;
        }

        SetMovement(currentDirection);
        return currentDirection;
    }

    public void ExecuteChase(Transform target, float chaseSpeed, float deadzoneX)
    {
        _movement.SetSpeed(chaseSpeed);
        float directionToPlayer = GetDirectionToPlayer(target, deadzoneX);

        if (_obstacleDetector.IsHittingWall)
        {
            _jumpCooldownTimer.Tick();
            if (_jump != null && _jumpCooldownTimer.IsFinished)
            {
                _jump.PerformJump();
                _jumpCooldownTimer.Start();
                SetMovement(directionToPlayer);
            }
            else
            {
                SetMovement(0f);
            }
        }
        else if (_obstacleDetector.IsAbyss)
        {
            SetMovement(0f);
            _jumpCooldownTimer.Tick();
        }
        else
        {
            SetMovement(directionToPlayer);
            _jumpCooldownTimer.Tick();
        }
    }

    public void StopMovement()
    {
        SetMovement(0f);
    }

    private void SetMovement(float direction)
    {
        _movement.SetDirection(direction);
        if (_orientation != null)
        {
            _orientation.SetFacingDirection(direction);
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
}