using System;
using UnityEngine;

public abstract class RuntimeVariable<T> : ScriptableObject
{
    [SerializeField] T _value;

    public T Value
    {
        get => _value;
        set
        {
            OnValueChanged?.Invoke(_value);
            _value = value;
        }
    }

    public event Action<T> OnValueChanged;
}