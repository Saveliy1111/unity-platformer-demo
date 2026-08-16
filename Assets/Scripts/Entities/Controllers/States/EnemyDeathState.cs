using UnityEngine;

public class EnemyDeathState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAIController aiController = animator.GetComponent<EnemyAIController>();

        if (aiController != null)
        {
            aiController.MovementComponent.SetDirection(0f);
        }
    }
}