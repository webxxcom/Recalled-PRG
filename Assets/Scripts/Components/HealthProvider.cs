using UnityEngine;

public abstract class ValueProvider : MonoBehaviour
{
    [field: SerializeField] public ValueProviderConfig Config { get; private set; }
    [SerializeField] ValueProviderSO _valueProviderSO;

    // Lazy init SO
    public ValueProviderSO Value => _valueProviderSO = _valueProviderSO != null ? _valueProviderSO : ScriptableObject.CreateInstance<ValueProviderSO>();
}

public class HealthProvider : ValueProvider
{
    [field: SerializeField] public bool IsInvincible { get; set; }

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

    private void Start()
    {
        Value.Init(Config);
    }

    public void DealDamage(GameObject changer, int value)
    {
        if (IsInvincible)
            return;

        Value.Change(changer, value);
    }
}
