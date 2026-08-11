using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthResource : ValueResource
{
    [SerializeField] bool _isInfinite;
    
    [Header("Prefabs")]
    [SerializeField] ParticleSystem _damageParticles;
    [SerializeField] PopupDamageText _damagePopup;

    public Collider2D Hurtbox { get; private set; }
    public EffectMachineSO EffectMachine { get; private set; }
    public bool IsInvincible { get; set; }

    public Action<DamageInfo> OnHpChanged;
    public Action<DamageInfo> OnDeath;
    public Action<DamageInfo> OnMax;

    protected override void Awake()
    {
        base.Awake();

        EffectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
        Hurtbox = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        OnHpChanged += Particles;
        OnHpChanged += ApplyKnockback;
        OnHpChanged += PopupDamage;
    }

    private void OnDisable()
    {
        OnHpChanged -= Particles;
        OnHpChanged -= ApplyKnockback;
        OnHpChanged -= PopupDamage;
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

    void PopupDamage(DamageInfo di)
    {
        Instantiate(_damagePopup, Hurtbox.bounds.center, Quaternion.identity).Init(di.Amount + "");
    }

    public bool IsDead => CurrentValue <= 0;

    public void ApplyDamage(DamageInfo damageInfo)
    {
        if (IsInvincible)
            return;

        int applied = Change(-damageInfo.Amount);
        if (applied == 0 && !_isInfinite)
            return;

        OnHpChanged?.Invoke(damageInfo);

        if (!_isInfinite)
        {
            if (CurrentValue == 0)
                OnDeath?.Invoke(damageInfo);
            if (CurrentValue == MaxValue)
                OnMax?.Invoke(damageInfo);
        }
    }
}
