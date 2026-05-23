using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
class ProjectileAttackComponent : MonoBehaviour, IAttackStrategy
{
    [SerializeField] GameObject projectilePrefab;

    new Collider2D collider2D;
    PlayerController player;
    EntityController entityController;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        entityController = GetComponentInParent<EntityController>();
    }

    public void SpawnProjectile()
    {
        GameObject arrow = Instantiate(projectilePrefab);
        ProjectileScript ps = arrow.GetComponent<ProjectileScript>();
        ps.StartPos = transform.position;
        ps.Destination = player.transform.position;
        ps.Owner = entityController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController pc))
        {
            player = pc;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController pc))
        {
            player = null;
        }
    }

    public void Execute()
    {
    }

    public bool CanBeExecuted()
    {
        return player != null;
    }
}
