using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public float KnockbackPower { get; private set; }
    public int Amount { get; private set; }
    public EntityController Source { get; private set; }
    public Collider2D Hitbox { get; private set; }
    public Collider2D Hurtbox { get; private set; }
    public List<EffectDefinition> Effects { get; private set; }
    public readonly Vector2 Direction => ((Vector2)Hurtbox.bounds.center - _origin).normalized;
    Vector2 _origin;

    public DamageInfo(int quantity, float knockbackPower, EntityController source,
        Collider2D hitbox, Collider2D hurtbox, List<EffectDefinition> effects)
    {
        Amount = quantity;
        KnockbackPower = knockbackPower;
        Source = source;
        Hitbox = hitbox;
        Hurtbox = hurtbox;
        Effects = effects;

        _origin = source.GetComponentInChildren<HealthResource>().Hurtbox.bounds.center;
    }
}