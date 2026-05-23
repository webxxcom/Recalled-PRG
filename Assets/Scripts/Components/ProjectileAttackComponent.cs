using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

[RequireComponent(typeof(EnemyAttackComponent))]
class ProjectileAttackComponent : MonoBehaviour, IAttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    new Collider2D collider2D;
    EntityController entityController;
    EnemyAttackComponent enemyAttackComponent;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        enemyAttackComponent = GetComponent<EnemyAttackComponent>();

        entityController = GetComponentInParent<EntityController>();
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
    }

    public bool CanBeExecuted()
    {
        return true;
    }
}
