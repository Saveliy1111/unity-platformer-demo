using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _chaseDirectionDeadzoneX = 0.2f;

    private EnemyAIController _aiController;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.gameObject.GetComponentInParent<EnemyAIController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;
        if (CheckLostAggro(animator)) return;

        if (_aiController.NavigationStrategy != null && _aiController.PlayerDetector.Target != null)
        {
            _aiController.NavigationStrategy.ExecuteChase(
                _aiController.PlayerDetector.Target, 
                _chaseSpeed, 
                _chaseDirectionDeadzoneX
            );

            float currentSpeed = _aiController.NavigationStrategy.IsMoving ? 1f : 0f;
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController != null && _aiController.NavigationStrategy != null)
        {
            _aiController.NavigationStrategy.StopMovement();
        }
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
}