using UnityEngine;

public class EnemyHurtState : StateMachineBehaviour
{
    private EnemyAIController _aiController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aiController = animator.gameObject.GetComponent<EnemyAIController>();

        if (_aiController != null)
        {
            _aiController.MovementComponent.SetDirection(0f);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_aiController != null && _aiController.MovementComponent != null)
        {
            _aiController.MovementComponent.enabled = true;
        }
    }
}