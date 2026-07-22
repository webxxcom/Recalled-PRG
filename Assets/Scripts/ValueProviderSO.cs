using System;
using UnityEngine;

[CreateAssetMenu(menuName = "HealthProvider")]
public class ValueProviderSO : ScriptableObject
{
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int CurrentValue { get; private set; }
    [field: SerializeField] public bool IsStatic { get; private set; }

    public event Action<GameObject, int> OnValueChanged;
    public event Action<GameObject> OnMinValueReached;
    public event Action<GameObject> OnMaxValueReached;

    public void Change(GameObject changer, int value)
    {
        if (!IsStatic)
        {
            if (CurrentValue + value <= 0)
            {
                CurrentValue = 0;

                OnMinValueReached.Invoke(changer);
            }
            else if (CurrentValue + value >= MaxValue)
            {
                CurrentValue = MaxValue;

                OnMaxValueReached.Invoke(changer);
            }
            else
                CurrentValue += value;
        }

        OnValueChanged.Invoke(changer, value);
    }

    public void Init(ValueProviderConfig cfg)
    {
        MaxValue = cfg.MaximumValue;
        CurrentValue = cfg.CurrentValue;
        IsStatic = cfg.IsStatic;
    }
}
