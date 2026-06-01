using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    [SerializeField] float impactTime = 0.3f;

    PlayerAttackComponent playerAttackComponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAttackComponent = animator.GetComponentInChildren<PlayerAttackComponent>();

        playerAttackComponent.StartAttackExecution();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > impactTime)
            playerAttackComponent.UpdateAttackExecution();
    }
}
