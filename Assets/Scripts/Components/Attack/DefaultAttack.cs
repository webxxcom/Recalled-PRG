using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DefaultAttack class describes the default data which every object which can deal damage must have
/// it includes data like knockback power, dealt damage, effects applied and hostile factions such as player or enemy.
/// </summary>
public abstract class DefaultAttack : MonoBehaviour
{
    [field: SerializeField] public AttackData AttackData { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }
    [field: SerializeField] public List<FactionDefinition> HostileFactions { get; private set; }
    public CapsuleCollider2D Hitbox { get; protected set; }

    public virtual int DealtDamage => AttackData.DealtDamage;
    public virtual float DealtKnockbackPower => AttackData.KnockbackPower;

    public event Action<HealthProvider> OnAttackApplied;

    protected virtual void Awake()
    {
    }

    protected virtual void OnEnable()
    {
        OnAttackApplied += ApplyKnockback;
    }

    protected virtual void OnDisable()
    {
        OnAttackApplied -= ApplyKnockback;
    }

    public void ApplyKnockback(HealthProvider target)
    {
        if (target.TryGetComponent(out ExternalVelocityComponent externalVelocityComponent))
        {
            Vector2 attackDir = (target.transform.position - gameObject.transform.position).normalized;

            externalVelocityComponent.Add(attackDir * DealtKnockbackPower);
        }
    }

    public void DealDamage(HealthProvider target, GameObject origin)
    {
        target.Change(origin, -DealtDamage);

        OnAttackApplied?.Invoke(target);
    }
}
