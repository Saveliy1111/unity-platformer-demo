using UnityEngine;

public class EnemyJumpState : StateMachineBehaviour
{
    [Header("Jump Settings")]
    private IEntityOrientation _orientation;
    private EnemyAIController _aiController;
    private EntityJump _jumpComponent;
    




    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.GetComponent<EnemyAIController>();
        _jumpComponent = animator.GetComponent<EntityJump>();
        _orientation = animator.GetComponent<IEntityOrientation>();

        if (_jumpComponent != null)
        {
            _jumpComponent.Jump();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController == null) return;

        float currentDirection = DirectionUtils.GetForwardVector(_aiController.transform, _orientation).x;
        _aiController.MovementComponent.SetDirection(currentDirection);     
    }

}
