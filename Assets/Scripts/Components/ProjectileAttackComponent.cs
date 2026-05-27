using UnityEngine;

[RequireComponent(typeof(EnemyAttackComponent))]
class ProjectileAttackComponent : MonoBehaviour, IAttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    EntityController entityController;
    EnemyAttackComponent enemyAttackComponent;

    private void Awake()
    {
        enemyAttackComponent = GetComponent<EnemyAttackComponent>();

        entityController = GetComponentInParent<EntityController>();
        if (!entityController)
            Debug.LogError("ProjectileAttackComponent requires EntityController in the parent");
    }

    public void SpawnProjectile()
    {
        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        ProjectileScript ps = arrow.GetComponent<ProjectileScript>();
        ps.Destination = enemyAttackComponent.CurrentTarget.transform.position;
        ps.Owner = entityController;
    }

    public void Execute()
    {
        SpawnProjectile();
    }

    public bool CanBeExecuted()
    {
        return true;
    }
}
