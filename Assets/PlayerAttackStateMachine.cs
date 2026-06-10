using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    [SerializeField] float impactTime = 0.3f;
    [SerializeField] float finishTime = 0.8f;
    [SerializeField] float speedMultiplier = 0.3f;

    DefaultAttackComponent defaultAttackComponent;
    MovementBase movementBase;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase = animator.GetComponent<MovementBase>();
        defaultAttackComponent = animator.GetComponentInChildren<DefaultAttackComponent>();

        defaultAttackComponent.StartAttackExecution();
        movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > impactTime && stateInfo.normalizedTime < finishTime)
            defaultAttackComponent.UpdateAttackExecution();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase.SpeedAggregator.Remove(speedMultiplier);
    }
}
