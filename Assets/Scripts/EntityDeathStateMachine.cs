using UnityEngine;

public class EntityDeathStateMachine : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.GetComponentInParent<EntityController>().gameObject);
    }
}
