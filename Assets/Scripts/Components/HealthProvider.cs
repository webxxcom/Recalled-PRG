using UnityEngine;

public class HealthProvider : ValueProvider
{
    [field: SerializeField] public bool IsInvincible { get; set; }

    public bool IsDead
    {
        get => Health.CurrentValue <= 0;
        set
        {
            if (value)
                Health.Change(gameObject, 0);
            else
                Health.Change(gameObject, Health.MaxValue);
        }
    }

    private void Start()
    {
        if (Health == null) // Create health SO instance for enemies and other entities
            Health = ScriptableObject.CreateInstance<ValueProviderSO>();

        Health.Init(Config);
    }

    public void DealDamage(GameObject changer, int value)
    {
        if (IsInvincible)
            return;

        Health.Change(changer, value);
    }
}
