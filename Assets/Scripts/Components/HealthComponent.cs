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

        //OnValueChanged += (obj, val) => ApplyKnockback(obj, 1.1f);
    }

    public override void Change(GameObject changer, int value)
    {
        if (!IsInvincible)
            base.Change(changer, value);
    }

    private void ApplyKnockback(GameObject attacker, float knockbackPower)
    {
        Vector2 forceVector = (transform.position - attacker.transform.position).normalized;

        rigidbody2D.AddForce(forceVector * knockbackPower, ForceMode2D.Impulse);
        //entityController.FreezeFor(0.2f);
    }
}
