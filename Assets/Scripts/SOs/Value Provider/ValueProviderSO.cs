using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ValueProviderSO")]
public class ValueProviderSO : ScriptableObject
{
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int CurrentValue { get; private set; }
    [field: SerializeField] public bool IsStatic { get; private set; }
    public bool Initialized { get; private set; }

    public void Change(int value)
    {
        if (IsStatic)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + value, 0, MaxValue);
    }

    public void Init(ValueProviderConfig cfg)
    {
        MaxValue = cfg.MaximumValue;
        CurrentValue = cfg.CurrentValue;
        IsStatic = cfg.IsStatic;

        Initialized = true;
    }
}
