using UnityEngine;

public class EnemyHurtState : StateMachineBehaviour
{
    private EnemyAIController _aiController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.gameObject.GetComponentInParent<EnemyAIController>();

        if (_aiController != null)
        {
            _aiController.MovementComponent.SetDirection(0);
        }
    }
}