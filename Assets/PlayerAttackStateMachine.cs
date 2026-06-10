using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    [SerializeField] float impactTime = 0.3f;

    PlayerAttackComponent playerAttackComponent;
    MovementBase movementBase;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase = animator.GetComponent<MovementBase>();
        playerAttackComponent = animator.GetComponentInChildren<PlayerAttackComponent>();

        playerAttackComponent.StartAttackExecution();
        movementBase.SpeedAggregator.Add(0.3f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > impactTime)
            playerAttackComponent.UpdateAttackExecution();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase.SpeedAggregator.Remove(0.3f);
    }
}
