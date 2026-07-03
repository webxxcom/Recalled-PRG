using UnityEngine;

public class EntityHurtStateMachine : StateMachineBehaviour
{
    MovementBase movementBase;
    readonly float speedMultiplier = 0.3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase = animator.GetComponent<MovementBase>();

        if (movementBase)
            movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movementBase)
            movementBase.SpeedAggregator.Remove(speedMultiplier);
    }
}
