using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private RandomTimer _patrolTimer;
    [SerializeField] private CooldownTimer _turnCooldownTimer = new CooldownTimer();
    [SerializeField] private float _turnCooldown = 0.3f;

    private EnemyAIController _aiController;
    private float _currentDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _aiController = animator.gameObject.GetComponentInParent<EnemyAIController>();

       if (_aiController != null)
       {
           _aiController.MovementComponent.SetSpeed(_patrolSpeed);
           _currentDirection = _aiController.Orientation.ForwardVector.x;
       }

       _patrolTimer.Start();
       _turnCooldownTimer.StartCooldown(0f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (_aiController == null) return;

        if (CheckForAggro(animator)) return;

        CheckForObstacles();
        _aiController.SetMovementDirection(_currentDirection);

        CheckTimer(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController != null)
        {
            _aiController.SetMovementDirection(0);
        }
    }

    private void CheckForObstacles()
    {
        if (_aiController.ObstaclesDetector != null)
            {
                if (_aiController.ObstaclesDetector.IsHittingWall || _aiController.ObstaclesDetector.IsLedge)
                {
                    _currentDirection *= -1;
                    _turnCooldownTimer.StartCooldown(_turnCooldown);
                }
            }
    }

    private bool CheckForAggro(Animator animator)
    {
        if (_aiController.PlayerDetector != null && _aiController.PlayerDetector.HasAggro)
        {
            animator.SetBool("isChasing", true);
            return true;
        }

        return false;
    }

    private void CheckTimer(Animator animator)
    {
        if (_patrolTimer.Tick())
        {
            animator.SetBool("isPatrolling", false);
        }
    }
}