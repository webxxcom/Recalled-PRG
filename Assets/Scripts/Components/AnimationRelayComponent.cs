using UnityEngine;

public class AnimationRelayComponent : MonoBehaviour
{
    [SerializeField] EntityAttackComponent entityAttackComponent;

    public void ExecuteAttack()
    {
        entityAttackComponent.ExecuteAttack();
    }
}
