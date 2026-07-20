using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
class ProjectileAttackComponent : AttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    public void SpawnProjectile()
    {

    }

    public override void Execute()
    {
        SpawnProjectile();
    }
}
