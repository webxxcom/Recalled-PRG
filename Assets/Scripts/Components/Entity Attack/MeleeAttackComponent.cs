using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EntityAttackComponent))]
public class MeleeAttackComponent : MonoBehaviour, IAttackStrategy
{
    EntityAttackComponent enemyAttackComponent;
    new Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        enemyAttackComponent = GetComponent<EntityAttackComponent>();

        if (!collider2D.isTrigger)
            Debug.LogError("The MeeleAttackComponents's collider must be a trigger");
    }

    public void Execute()
    {
        GameObject target = enemyAttackComponent.CurrentTarget;

        if (!target)
            return;

        HealthComponent hc = target.GetComponentInParent<HealthComponent>();
        hc.TakeDamage(gameObject, 10, 0.1f);
    }
}
