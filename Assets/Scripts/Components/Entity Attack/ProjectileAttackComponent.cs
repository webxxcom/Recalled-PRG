using UnityEngine;

[RequireComponent(typeof(EnemyAttackComponent))]
class ProjectileAttackComponent : AttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    public void SpawnProjectile()
    {
        if (!entityAttackComponent.Target)
            return;

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<ProjectileScript>()
            .Initialize(entityController, entityAttackComponent.Target.transform.position);
    }

    public override void Execute()
    {
        SpawnProjectile();
    }
}
