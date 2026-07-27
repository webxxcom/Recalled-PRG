using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Attack Data")]
public class AttackSO : ScriptableObject
{
    [field: SerializeField] public virtual int DealtDamage { get; private set; } = 10;
    [field: SerializeField] public virtual float ReloadTime { get; private set; } = 0.8f;
    [field: SerializeField] public virtual float KnockbackPower { get; private set; } = 1.6f;
    [field: SerializeField] public Transform KnockbackOriginTransform { get; set; }
    [field: SerializeField] public float ImpactTime { get; private set; } = 0.3f;
    [field: SerializeField] public float RecoveryTime { get; private set; } = 0.8f;
    [field: SerializeField] public float SpeedMultiplier { get; private set; } = 0.3f;
    [field: SerializeField] public AttackCurvesSO Curves { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }

    void ApplyKnockback(HealthProvider target)
    {
        if (target.TryGetComponent(out MovementBase movementBase))
        {
            Vector2 attackDir = (target.transform.position - KnockbackOriginTransform.position).normalized;

            movementBase.AddExternalVelocity(attackDir * KnockbackPower);
        }
    }

    public void DealDamage(HealthProvider target, GameObject origin)
    {
        target.DealDamage(origin, -DealtDamage);

        ApplyKnockback(target);
    }
}
