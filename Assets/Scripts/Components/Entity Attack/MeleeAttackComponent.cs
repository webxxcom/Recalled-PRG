using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EntityAttackComponent))]
public class MeleeAttackComponent : MonoBehaviour, IAttackStrategy
{
    EntityAttackComponent enemyAttackComponent;
    EntityController entityController;

    private void Awake()
    {
        enemyAttackComponent = GetComponent<EntityAttackComponent>();
        entityController = GetComponentInParent<EntityController>();
    }

    public void Execute()
    {
        PlayerController target = enemyAttackComponent.PlayerController;

        if (!target)
            return;
        target.HealthComponent.Change(entityController.gameObject, -10);
    }
}
