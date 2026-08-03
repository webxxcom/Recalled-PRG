using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthProvider : ValueProvider
{
    public bool IsInvincible { get; set; }

    [SerializeField] EntityController _entityController;
    [SerializeField] ParticleSystem _damageParticles;
    [SerializeField] Animator _animator;
    [SerializeField] Collider2D _hurtbox;
    public EffectMachineSO EffectMachine { get; private set; }

    public event UnityAction<DamageInfo> OnHpChanged;
    public event UnityAction<DamageInfo> OnMinHpReached;

    private void Awake()
    {
        EffectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
    }

    private void OnEnable()
    {
        OnHpChanged += Particles;
        OnHpChanged += ApplyKnockback;
    }

    void Particles(DamageInfo di)
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, di.Direction);

        Instantiate(_damageParticles, di.Hurtbox.bounds.center, rot);
    }

    void ApplyKnockback(DamageInfo di)
    {
        MovementBase movementBase = di.Hurtbox.GetComponentInParent<MovementBase>();

        if (movementBase)
            movementBase.AddExternalVelocity(di.Direction * di.KnockbackPower);
    }

    public bool IsDead
    {
        get => Value.CurrentValue <= 0;
        set
        {
            if (value)
                Value.Change(0);
            else
                Value.Change(Value.MaxValue);
        }
    }

    void RaiseEvents(DamageInfo di)
    {
        OnHpChanged?.Invoke(di);
        if (Value.CurrentValue == 0)
            OnMinHpReached?.Invoke(di);
    }

    public void DealDamage(int damage)
    {
        if (IsInvincible)
            return;

        Value.Change(damage);
    }

    public void DealDamage(DamageInfo damageInfo)
    {
        if (IsInvincible)
            return;

        Value.Change(damageInfo.Amount);
        RaiseEvents(damageInfo);
    }
}
