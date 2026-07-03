using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(Rigidbody2D))]
public class HealthComponent : ValueProvider
{
    [field: SerializeField] public bool IsInvincible { get; set; } = false;

    public bool IsDead
    {
        get => Value <= MinValue;
        set
        {
            if (value)
                Change(entityController.gameObject, -Value);
            else
                Change(entityController.gameObject, MaxValue);
        }
    }


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
