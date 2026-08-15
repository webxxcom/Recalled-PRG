using Antlr4.Runtime.Tree;
using System;
using UnityEngine;

public abstract class ValueResource : MonoBehaviour
{
    [SerializeField] ValueProviderConfig _config;

    [SerializeField] int _maxValue;
    [SerializeField] IntVariable _currentValue;
    [SerializeField] bool _isInfinite;

    public int MaxValue => _maxValue;
    public int CurrentValue => _currentValue.Value;

    /// <summary> (oldVal, newVal) after the change </summary>
    public event Action<int, int> OnValueChanged;
    public event Action<int> OnMinValue;
    public event Action<int> OnMaxValue;

    protected virtual void Awake()
    {
        //TODO ???
        //_maxValue = _config.MaximumValue;
        //_currentValue = _config.CurrentValue;
        //_isStatic = _config.IsStatic;

        _currentValue.Value = MaxValue;
    }

    /// <returns>The delta actually applied, after clamping</returns>
    public int Change(int delta)
    {
        if (delta == 0)
            return 0;

        int oldVal = _currentValue.Value;
        int newVal = Mathf.Clamp(_currentValue.Value + delta, 0, _maxValue);
        if (!_isInfinite)
            _currentValue.Value = newVal;

        int applied = oldVal - newVal;
        if (applied == 0)
            return 0;

        OnValueChanged?.Invoke(oldVal, newVal);

        if (oldVal != 0 && newVal == 0)
            OnMinValue?.Invoke(oldVal);
        else if (oldVal != MaxValue && newVal == _maxValue)
            OnMaxValue?.Invoke(oldVal);

        return applied;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_config != null)
        {
            if (_currentValue == null)
                _currentValue = ScriptableObject.CreateInstance<IntVariable>();

            _maxValue = _config.MaximumValue;
            _currentValue.Value = _maxValue;
            _isInfinite = _config.IsInfinite;
        }
    }
#endif
}
