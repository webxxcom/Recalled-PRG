using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
class ProjectileAttackComponent : AttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    public void SpawnProjectile()
    {
        if (!_entityAttack.Target)
            return;

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<ProjectileScript>()
            .Initialize(_entityController, _entityAttack.Target.transform.position);
    }

    public override void Execute()
    {
        SpawnProjectile();
    }
}
