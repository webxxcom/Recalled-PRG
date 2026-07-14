using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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


    Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Change(GameObject changer, int value)
    {
        if (IsInvincible)
            value = 0;

        base.Change(changer, value);
    }
}
