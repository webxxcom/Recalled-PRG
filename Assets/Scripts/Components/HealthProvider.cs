using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthProvider : ValueProvider
{
    private static readonly int HurtHash = Animator.StringToHash("Hurt");

    public bool IsInvincible { get; set; }

    [SerializeField] EntityController _entityController;
    [SerializeField] Animator _animator;
    EffectMachineSO _effectMachine;

    public event UnityAction<DamageInfo> OnHpChanged;
    public event UnityAction<DamageInfo> OnMinHpReached;

    private void Awake()
    {
        _effectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
    }

    private void OnEnable()
    {
        OnHpChanged += HpChanged;
    }

    private void OnDisable()
    {
        OnHpChanged -= HpChanged;
    }

    void HpChanged(DamageInfo damageInfo)
    {
        _animator.SetTrigger(HurtHash);
        damageInfo.Effects?.ForEach(e => _effectMachine.ApplyEffect(_entityController, this, e));
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

    public void DealDamage(GameObject changer, int value, List<EffectDefinition> effects = null)
    {
        if (IsInvincible)
            return;

        Value.Change(-value);
        RaiseEvents(new() { Amount = value, Source = changer, Effects = effects });
    }
}
