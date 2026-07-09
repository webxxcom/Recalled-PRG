using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider2D))]
public class TrapSawAttack : DefaultAttack
{
    new Collider2D collider2D;

    protected override void Awake()
    {
        base.Awake();

        collider2D = GetComponent<Collider2D>();
        knockbackOriginPosition = collider2D.transform;
    }

    protected override void HandleOnTriggerEnter2D(Collider2D collision)
    {
        EntityController ec = collision.GetComponentInParent<EntityController>();

        if (ec)
        {
            ec.HealthComponent.Change(gameObject, -DealtDamage);

            OnAttackApplied?.Invoke(ec);
        }
    }
}
