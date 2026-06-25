using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    [SerializeField] float impactTime = 0.3f;
    [SerializeField] float finishTime = 0.8f;
    [SerializeField] float speedMultiplier = 0.3f;

    EntityController entityController;
    DefaultAttackComponent defaultAttackComponent;
    MovementBase movementBase;

    void CacheAll(Animator animator)
    {
        if (!entityController)
            entityController = animator.GetComponentInParent<EntityController>();

        if (!movementBase)
            movementBase = entityController.GetComponent<MovementBase>();

        if (!defaultAttackComponent)
            defaultAttackComponent = entityController.GetComponentInChildren<DefaultAttackComponent>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CacheAll(animator);

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
