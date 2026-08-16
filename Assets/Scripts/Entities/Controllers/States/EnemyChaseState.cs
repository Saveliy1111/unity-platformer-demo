using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private RandomTimer _jumpCooldownTimer;

    [Header("Tolerances")]
    [SerializeField] private float _chaseDirectionDeadzoneX = 0.2f;
    [SerializeField] private float _yMinDepthForSafeDrop = 0.2f;

    private EnemyAIController _aiController;
    private EntityJump _jumpComponent;
    private IEntityOrientation _orientation;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.gameObject.GetComponent<EnemyAIController>();
        _jumpComponent = animator.gameObject.GetComponent<EntityJump>();
        _orientation = animator.GetComponent<IEntityOrientation>();

        if (_aiController != null)
        {
            _aiController.MovementComponent.SetSpeed(_chaseSpeed);
        }

        _jumpCooldownTimer.Start();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;

        if (CheckLostAggro(animator)) return;

        ChaseTarget(animator);
    }

    private bool CheckLostAggro(Animator animator)
    {
        if (_aiController.PlayerDetector == null || 
            !_aiController.PlayerDetector.HasAggro || 
            _aiController.PlayerDetector.Target == null)
        {
            animator.SetBool("isChasing", false);
            return true;
        }
        return false;
    }

    private void ChaseTarget(Animator animator)
    {
        float directionToPlayer = GetDirectionToPlayer();
        HandleObstacles(animator, directionToPlayer);
    }

    private float GetDirectionToPlayer()
    {
        float distanceX = _aiController.PlayerDetector.Target.position.x - _aiController.transform.position.x;
        float directionToPlayer;

        if (Mathf.Abs(distanceX) < _chaseDirectionDeadzoneX)
        {
            directionToPlayer = DirectionUtils.GetForwardVector(_aiController.transform, _orientation).x;
        }
        else
        {
            directionToPlayer = Mathf.Sign(distanceX);
        }

        return directionToPlayer;
    }

    private void HandleObstacles(Animator animator, float directionToPlayer)
    {
        if (_aiController.ObstaclesDetector == null) return;

        if (_aiController.ObstaclesDetector.IsHittingWall)
        {
            HandleWall(animator, directionToPlayer);
        }
        else if (_aiController.ObstaclesDetector.IsAbyss)
        {
            HandleAbyss(animator);
        }
        else if (_aiController.ObstaclesDetector.IsSafeDrop)
        {
            HandleSafeDrop(directionToPlayer);
        }
        else
        {
            HandleClearPath(directionToPlayer);
        }
    }

    private void HandleWall(Animator animator, float directionToPlayer)
    {
        bool isJumpReady = _jumpCooldownTimer.Tick();
        if (_jumpComponent != null && isJumpReady)
        {
            animator.SetTrigger("jump");
            _jumpCooldownTimer.Start();
            _aiController.MovementComponent.SetDirection(directionToPlayer);
        }
        else
        {
            _aiController.MovementComponent.SetDirection(0);
        }
    }

    private void HandleAbyss(Animator animator)
    {
        _aiController.MovementComponent.SetDirection(0);
        animator.SetBool("isChasing", false);
        _jumpCooldownTimer.Tick();
    }

    private void HandleSafeDrop(float directionToPlayer)
    {
        _aiController.MovementComponent.SetDirection(directionToPlayer);
        
        _jumpCooldownTimer.Tick();
    }

    private void HandleClearPath(float directionToPlayer)
    {
        _aiController.MovementComponent.SetDirection(directionToPlayer);
        _jumpCooldownTimer.Tick();
    }
}