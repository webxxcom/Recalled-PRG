using UnityEngine;
using UnityEngine.Events;

public abstract class ValueProvider<T> : MonoBehaviour
{
    [SerializeField] ValueProviderConfig _config;
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int CurrentValue { get; private set; }
    [field: SerializeField] public bool IsStatic { get; private set; }
    [field: SerializeField] public bool IsInfinite { get; private set; }

    public event UnityAction<T> OnValueChanged;
    public event UnityAction<T> OnMinValue;
    public event UnityAction<T> OnMaxValue;

    private void OnValidate()
    {
        if (_config != null)
        {
            MaxValue = _config.MaximumValue;
            CurrentValue = _config.CurrentValue;
            IsStatic = _config.IsStatic;
            IsInfinite = _config.IsInfinite;
        }
    }

    public void Change(int value, T data)
    {
        if (IsStatic)
            return;

        if (!IsInfinite)
        {
            if (CurrentValue + value >= MaxValue)
            {
                CurrentValue = MaxValue;
                OnMaxValue?.Invoke(data);
            }
            else if (CurrentValue + value <= 0)
            {
                CurrentValue = 0;
                OnMinValue?.Invoke(data);
            }
            else
                CurrentValue += value;
        }

        OnValueChanged?.Invoke(data);
    }

    public void Init()
    {
        MaxValue = _config.MaximumValue;
        CurrentValue = _config.CurrentValue;
        IsStatic = _config.IsStatic;
        IsInfinite = _config.IsInfinite;
    }
}
