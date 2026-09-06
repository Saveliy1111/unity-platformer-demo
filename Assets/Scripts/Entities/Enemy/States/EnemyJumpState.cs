using UnityEngine;

public class EnemyJumpState : StateMachineBehaviour
{
    [Header("Jump Settings")]
    private EnemyAIController _aiController;
    private Jump _jumpComponent;
    private IMovement _movementComponent;
    private float _jumpDirection;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.GetComponentInParent<EnemyAIController>();
        _jumpComponent = animator.GetComponentInParent<Jump>();
        _movementComponent = animator.GetComponentInParent<IMovement>();

        if (_aiController != null && _aiController.NavigationStrategy != null)
        {
            _jumpDirection = _aiController.NavigationStrategy.CurrentDirection;
        }

        if (_jumpComponent != null)
        {
            _jumpComponent.PerformJump();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_movementComponent != null)
        {
            _movementComponent.SetDirection(_jumpDirection);
        }
    }

}
