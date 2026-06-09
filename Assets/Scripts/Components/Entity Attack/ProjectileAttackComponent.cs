using UnityEngine;

[RequireComponent(typeof(EntityAttackComponent))]
class ProjectileAttackComponent : MonoBehaviour, IAttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    EntityController entityController;
    EntityAttackComponent enemyAttackComponent;

    private void Awake()
    {
        enemyAttackComponent = GetComponent<EntityAttackComponent>();

        entityController = GetComponentInParent<EntityController>();
        if (!entityController)
            Debug.LogError("ProjectileAttackComponent requires EntityController in the parent");
    }

    public void SpawnProjectile()
    {
        if (!enemyAttackComponent.PlayerController)
            return;

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<ProjectileScript>()
            .Initialize(entityController, enemyAttackComponent.PlayerController.transform.position);
    }

    public void Execute()
    {
        SpawnProjectile();
    }
}
