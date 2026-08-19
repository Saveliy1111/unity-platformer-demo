using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    [Header("Idle Settings")]
    [SerializeField] private RandomTimer _idleTimer;

    private EnemyAIController _aiController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.gameObject.GetComponentInParent<EnemyAIController>();

        if (_aiController != null)
        {
            _aiController.MovementComponent.SetDirection(0);
        }

        _idleTimer.Start();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;

       if (CheckForAggro(animator)) return;
       CheckTimer(animator);
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
        if (_idleTimer.Tick())
        {
            animator.SetBool("isPatrolling", true);
        }
    }
}