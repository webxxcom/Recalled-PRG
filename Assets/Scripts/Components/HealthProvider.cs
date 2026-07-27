using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthProvider : ValueProvider
{
    public bool IsInvincible { get; set; }

    [SerializeField] EntityController _entityController;
    [SerializeField] EffectMachineSO _effectMachine;

    public event UnityAction<DamageInfo> OnHpChanged;
    public event UnityAction<DamageInfo> OnMinHpReached;

    private void Awake()
    {
        _effectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
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

    public void RaiseEvents(GameObject changer, int value)
    {
        DamageInfo di = new() { Amount = value, Source = changer };

        OnHpChanged?.Invoke(di);
        if (Value.CurrentValue == 0)
            OnMinHpReached?.Invoke(di);
    }

    public void DealDamage(GameObject changer, int value, List<EffectDefinition> effects = null)
    {
        if (IsInvincible)
            return;

        Value.Change(value);
        effects?.ForEach(e => _effectMachine.ApplyEffect(_entityController, this, e));
        RaiseEvents(changer, value);
    }
}
