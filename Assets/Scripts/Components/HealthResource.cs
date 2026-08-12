using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthResource : ValueResource
{
    [SerializeField] bool _isInfinite;
    
    public Collider2D Hurtbox { get; private set; }
    public EffectMachineSO EffectMachine { get; private set; }
    public bool IsInvincible => _invincibilityTimer > 0f;
    float _invincibilityTimer;

    public event Action<DamageInfo> OnHpChangeApplied;
    public event Action<DamageInfo> OnHpChange;
    public event Action<DamageInfo> OnDeath;
    public event Action<DamageInfo> OnMax;

    protected override void Awake()
    {
        base.Awake();

        EffectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
        Hurtbox = GetComponent<Collider2D>();
    }

    public bool IsDead => CurrentValue <= 0;

    public void ApplyDamage(DamageInfo damageInfo)
    {
        if (IsInvincible)
            return;

        OnHpChange?.Invoke(damageInfo);
        int applied = Change(-damageInfo.Amount);
        if (applied == 0 && !_isInfinite)
            return;

        damageInfo.Amount = applied;
        OnHpChangeApplied?.Invoke(damageInfo);

        if (!_isInfinite)
        {
            if (CurrentValue == 0)
                OnDeath?.Invoke(damageInfo);
            if (CurrentValue == MaxValue)
                OnMax?.Invoke(damageInfo);
        }
    }

    public void GrantInvincibility(float time)
        => _invincibilityTimer = Mathf.Max(_invincibilityTimer, time);

    private void Update()
    {
        if (_invincibilityTimer > 0f)
            _invincibilityTimer -= Time.deltaTime;
    }
}
