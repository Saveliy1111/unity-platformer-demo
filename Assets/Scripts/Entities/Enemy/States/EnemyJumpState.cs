using UnityEngine;

public class EnemyJumpState : StateMachineBehaviour
{
    [Header("Jump Settings")]
    private EnemyAIController _aiController;
    private Jump _jumpComponent;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.GetComponentInParent<EnemyAIController>();
        _jumpComponent = animator.GetComponentInParent<Jump>();

        if (_jumpComponent != null)
        {
            _jumpComponent.PerformJump();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;

        float currentDirection = _aiController.Orientation.ForwardVector.x;
        _aiController.SetMovementDirection(currentDirection);     
    }

}
