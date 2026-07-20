using UnityEngine;

public class HealthProvider : ValueProvider
{
    [field: SerializeField] public bool IsInvincible { get; set; } = false;

    public bool IsDead
    {
        get => Value <= MinValue;
        set
        {
            if (value)
                Change(gameObject, -Value);
            else
                Change(gameObject, MaxValue);
        }
    }

    public override void Change(GameObject changer, int value)
    {
        if (IsInvincible)
            value = 0;

        base.Change(changer, value);
    }
}
