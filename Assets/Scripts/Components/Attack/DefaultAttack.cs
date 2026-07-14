using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackData
{
    [field: SerializeField] public int DealtDamage { get; set; }
    [field: SerializeField] public float KnockbackPower { get; set; }
}

public abstract class DefaultAttack : MonoBehaviour
{
    [field: SerializeField] protected AttackData BasicAttackData { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }
    [field: SerializeField] public List<FactionDefinition> HostileFactions { get; private set; }

    public abstract int DealtDamage { get; }
    public abstract float DealtKnockbackPower { get; }

    protected Transform knockbackOriginPosition;

    public Action<HealthProvider> OnAttackApplied;

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
            Vector2 attackDir = (target.transform.position - knockbackOriginPosition.position).normalized;

            externalVelocityComponent.Add(attackDir * DealtKnockbackPower);
        }
    }

    public void DealDamage(HealthProvider target, GameObject origin)
    {
        target.Change(origin, -DealtDamage);

        OnAttackApplied?.Invoke(target);
    }
}
