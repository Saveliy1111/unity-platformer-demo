using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private RandomTimer _patrolTimer;

    private EnemyAIController _aiController;
    private float _currentDirection; 

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _aiController = animator.gameObject.GetComponentInParent<EnemyAIController>();

       if (_aiController != null && _aiController.NavigationStrategy != null)
       {
           _currentDirection = _aiController.NavigationStrategy.CurrentDirection;
       }

       _patrolTimer.Start();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;
        if (CheckForAggro(animator)) return;

        if (_aiController.NavigationStrategy != null)
        {
            _currentDirection = _aiController.NavigationStrategy.ExecutePatrol(_currentDirection, _patrolSpeed);
        }

        _patrolTimer.Tick();
        if (_patrolTimer.IsFinished)
        {
            animator.SetBool("isPatrolling", false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController != null && _aiController.NavigationStrategy != null)
        {
            _aiController.NavigationStrategy.StopMovement();
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
}