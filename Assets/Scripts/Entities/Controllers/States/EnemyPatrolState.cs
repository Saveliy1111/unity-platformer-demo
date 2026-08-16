using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private RandomTimer _patrolTimer;

    private EnemyAIController _aiController;
    private IEntityOrientation _orientation;
    private float _currentDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _aiController = animator.gameObject.GetComponent<EnemyAIController>();
       _orientation = animator.GetComponent<IEntityOrientation>();

       if (_aiController != null)
       {
           _aiController.MovementComponent.SetSpeed(_patrolSpeed);
           _currentDirection = DirectionUtils.GetForwardVector(_aiController.transform, _orientation).x;
       }

       _patrolTimer.Start();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (_aiController == null) return;

        if (CheckForAggro(animator)) return;

        CheckForObstacles();
        _aiController.MovementComponent.SetDirection(_currentDirection);

        CheckTimer(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController != null)
        {
            _aiController.MovementComponent.SetDirection(0);
        }
    }

    private void CheckForObstacles()
    {
        if (_aiController.ObstaclesDetector != null)
            {
                if (_aiController.ObstaclesDetector.IsHittingWall || _aiController.ObstaclesDetector.IsLedge)
                {
                    _currentDirection *= -1;
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