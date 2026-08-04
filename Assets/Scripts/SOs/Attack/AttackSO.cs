using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Attack Data")]
public class AttackSO : ScriptableObject
{
    [field: SerializeField] public virtual int DealtDamage { get; private set; } = 10;
    [field: SerializeField] public virtual float ReloadTime { get; private set; } = 0.8f;
    [field: SerializeField] public virtual float KnockbackPower { get; private set; } = 1.6f;
    [field: SerializeField] public virtual float SpeedMultiplier { get; private set; } = 0.3f;
    [field: SerializeField] public float ImpactTime { get; private set; } = 0.3f;
    [field: SerializeField] public float RecoveryTime { get; private set; } = 0.8f;
    [field: SerializeField] public AttackCurvesSO Curves { get; private set; }
    [field: SerializeField] public List<EffectDefinition> Effects { get; private set; }

    public void DealDamage(GameObject source, Collider2D hitbox, Collider2D hurtbox)
    {
        HealthProvider target = hurtbox.GetComponentInParent<HealthProvider>();

        if (!target)
            return;

        target.DealDamage(new DamageInfo(-DealtDamage, KnockbackPower, source, hitbox, hurtbox, Effects));
    }
}
