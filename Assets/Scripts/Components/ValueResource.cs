using System;
using UnityEngine;

public abstract class ValueResource : MonoBehaviour
{
    [SerializeField] ValueProviderConfig _config;

    [SerializeField] int _maxValue;
    [SerializeField] int _currentValue;
    [SerializeField] bool _isStatic;

    public int MaxValue => _maxValue;
    public int CurrentValue => _currentValue;

    /// <summary> (oldVal, newVal) after the change </summary>
    public event Action<int, int> OnValueChanged;
    public event Action<int> OnMinValue;
    public event Action<int> OnMaxValue;

    protected virtual void Awake()
    {
        _maxValue = _config.MaximumValue;
        _currentValue = _config.CurrentValue;
        _isStatic = _config.IsStatic;
    }

    /// <returns>The delta actually applied, after clamping</returns>
    public int Change(int delta)
    {
        if (_isStatic || delta == 0)
            return 0;

        int prevVal = _currentValue;
        _currentValue = Mathf.Clamp(_currentValue + delta, 0, _maxValue);

        int applied = prevVal - _currentValue;
        if (applied == 0)
            return 0;

        OnValueChanged?.Invoke(prevVal, _currentValue);

        if (prevVal != 0 && _currentValue == 0)
            OnMinValue?.Invoke(prevVal);
        else if (prevVal != MaxValue && _currentValue == _maxValue)
            OnMaxValue?.Invoke(prevVal);

        return applied;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_config != null)
        {
            _maxValue = _config.MaximumValue;
            _currentValue = _config.CurrentValue;
            _isStatic = _config.IsStatic;
        }
    }
#endif
}
