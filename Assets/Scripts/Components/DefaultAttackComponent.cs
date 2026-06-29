using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class DefaultAttackComponent : MonoBehaviour
{
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public List<EffectAsset> Effects { get; private set; }
    [field: SerializeField] public List<FactionSO> HostileFactions { get; private set; }

    protected  Transform knockbackOriginPosition;

    public Action<EntityController> OnAttackApplied;

    protected virtual void Awake()
    {
        OnAttackApplied += ApplyKnockback;
    }

    public void ApplyKnockback(EntityController target)
    {
        Vector2 attackDir = (target.transform.position - knockbackOriginPosition.position).normalized;

        if (target.TryGetComponent(out ExternalVelocityComponent externalVelocityComponent))
            externalVelocityComponent.Add(attackDir * KnockbackPower);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If hitbox is triggered then check if we're hostile to this faction
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            FactionComponent fc = collision.GetComponentInParent<FactionComponent>();
            if (!fc || !HostileFactions.Contains(fc.Faction))
                return;

            HandleOnTriggerEnter2D(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            HandleOnTriggerExit2D(collision);
        }
    }

    protected virtual void HandleOnTriggerEnter2D(Collider2D collision)
    {

    }
    protected virtual void HandleOnTriggerExit2D(Collider2D collision)
    {

    }
}
