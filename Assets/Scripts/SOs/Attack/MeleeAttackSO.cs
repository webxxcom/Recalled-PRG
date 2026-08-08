using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack / Melee Attack Data")]
public class MeleeAttackSO : AttackSO
{
    [field: SerializeField] public virtual int DealtDamage { get; private set; } = 10;
    [field: SerializeField] public virtual float KnockbackPower { get; private set; } = 1.6f;
    [field: SerializeField] public float ImpactTime { get; private set; } = 0.3f;
    [field: SerializeField] public float RecoveryTime { get; private set; } = 0.8f;
    [field: SerializeField] public AttackCurvesSO Curves { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }

    public void DealDamage(GameObject source, Collider2D hitbox, Collider2D hurtbox)
    {
        if (hurtbox.TryGetComponent(out HealthProvider target))
            target.DealDamage(new DamageInfo(-DealtDamage, KnockbackPower, source, hitbox, hurtbox, Effects));
    }
}
