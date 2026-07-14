using UnityEngine;

public class PopupAttackComponent : AttackStrategy
{
    public override void Execute()
    {
        PlayerController target = entityAttackComponent.Target;

        if (!target || entityAttackComponent.DamagedTargets.Contains(target))
            return;

        entityAttackComponent.DamagedTargets.Add(target);
        target.HealthComponent.Change(entityController.gameObject, -entityAttackComponent.BasicDealtDamage);
    }
}
