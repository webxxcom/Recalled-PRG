using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MeleeAttackComponent : MonoBehaviour, IAttackStrategy
{
    EnemyAttackComponent enemyAttackComponent;
    new Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        enemyAttackComponent = GetComponent<EnemyAttackComponent>();

        if (!collider2D.isTrigger)
            Debug.LogError("The MeeleAttackComponents's collider must be a trigger");
    }

    public bool CanBeExecuted()
    {
        return true;
    }

    public void Execute()
    {
        HealthComponent hc = enemyAttackComponent.CurrentTarget.GetComponent<HealthComponent>();

        hc.TakeDamage(gameObject, 10, 1.3f);
    }
}
