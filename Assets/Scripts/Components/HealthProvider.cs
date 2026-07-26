using UnityEngine;

public abstract class ValueProvider : MonoBehaviour
{
    [SerializeField] ValueProviderConfig _config;
    [SerializeField] ValueProviderSO _valueProviderSO;

    // Lazy init SO
    public ValueProviderSO Value
    {
        get
        {
            if (_valueProviderSO == null)
                _valueProviderSO = ScriptableObject.CreateInstance<ValueProviderSO>();

            if (!_valueProviderSO.Initialized)
                _valueProviderSO.Init(_config);
            return _valueProviderSO;
        }
    }
}

public class HealthProvider : ValueProvider
{
    public bool IsInvincible { get; set; }

    public bool IsDead
    {
        get => Value.CurrentValue <= 0;
        set
        {
            if (value)
                Value.Change(gameObject, 0);
            else
                Value.Change(gameObject, Value.MaxValue);
        }
    }

    public void DealDamage(GameObject changer, int value)
    {
        if (IsInvincible)
            return;

        Value.Change(changer, value);
    }
}
