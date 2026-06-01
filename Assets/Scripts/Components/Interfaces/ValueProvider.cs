using UnityEngine;
using System;

public abstract class ValueProvider : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; protected set; }
    [field: SerializeField] public int MinValue { get; protected set; }
    [field: SerializeField] public int Value { get; protected set; }

    public event Action<GameObject, int> OnValueChanged;
    public event Action<GameObject> OnMinValueReached;
    public event Action<GameObject> OnMaxValueReached;

    private void Start()
    {
        Value = MaxValue;
    }

    public void Change(GameObject changer, int value)
    {
        if (Value + value <= MinValue)
        {
            Value = MinValue;

            OnMinValueReached?.Invoke(changer);
        }
        else if (Value + value >= MaxValue)
        {
            Value = MaxValue;

            OnMaxValueReached?.Invoke(changer);
        }
        else
            Value += value;

        OnValueChanged?.Invoke(changer, value);
    }
}
