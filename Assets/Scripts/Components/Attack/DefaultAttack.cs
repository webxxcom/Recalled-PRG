using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DefaultAttack class describes the default data which every object which can deal damage must have
/// it includes data like knockback power, dealt damage, effects applied and hostile factions such as player or enemy.
/// </summary>
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class DefaultAttack : MonoBehaviour
{
    [field: SerializeField] public AttackData AttackData { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }
    
    public CapsuleCollider2D Hitbox { get; protected set; }

    public event Action<HealthProvider> OnAttackApplied;

    protected virtual void Awake()
    {
        Hitbox = GetComponent<CapsuleCollider2D>();
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

            externalVelocityComponent.Add(attackDir * AttackData.KnockbackPower);
        }
    }

    public void DealDamage(HealthProvider target, GameObject origin)
    {
        target.DealDamage(origin, -AttackData.DealtDamage);

        OnAttackApplied?.Invoke(target);
    }
}
