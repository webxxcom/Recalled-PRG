using UnityEngine;

[RequireComponent(typeof(EntityAttackComponent))]
class ProjectileAttackComponent : AttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    private void Awake()
    {
        entityAttackComponent = GetComponent<EntityAttackComponent>();

        entityController = GetComponentInParent<EntityController>();
        if (!entityController)
            Debug.LogError("ProjectileAttackComponent requires EntityController in the parent");
    }

    public void SpawnProjectile()
    {
        if (!entityAttackComponent.PlayerController)
            return;

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<ProjectileScript>()
            .Initialize(entityController, entityAttackComponent.PlayerController.transform.position);
    }

    public override void Execute()
    {
        SpawnProjectile();
    }
}
