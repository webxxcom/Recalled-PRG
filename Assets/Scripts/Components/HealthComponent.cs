using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(Rigidbody2D))]
public class HealthComponent : ValueProvider
{
    [field: SerializeField] public bool IsInvincible { get; set; } = false;

    EntityController entityController;
    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        entityController = GetComponent<EntityController>();
    }

    public override void Change(GameObject changer, int value)
    {
        if (IsInvincible)
            value = 0;

        base.Change(changer, value);
    }
}
