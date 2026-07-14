using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackComponent : AttackStrategy
{
    public override void Execute()
    {
        PlayerController target = entityAttackComponent.Target;

        if (!target)
            return;
        target.HealthComponent.Change(entityController.gameObject, -entityAttackComponent.BasicDealtDamage);
    }

    private void Update()
    {
        SetAttackCollisionOffset();
    }
}
