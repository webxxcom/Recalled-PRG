using UnityEngine;

public class TrainingDummyController : EntityController
{
    private static readonly int DamageHardHash = Animator.StringToHash("HurtHard");
    private static readonly int DamageMidHash = Animator.StringToHash("HurtMid");
    private static readonly int DamageLightHash = Animator.StringToHash("HurtLight");

    [SerializeField] HealthResource _health;
    [SerializeField] Animator _animator;

    void HpChanged(int oldVal, int newVal)
    {
        // Calcualte this way to avoid healing improper animation
        int amount = newVal - oldVal;
        if (amount < 0)
        {
            if (amount >= -10)
                _animator.SetTrigger(DamageLightHash);
            else if (amount >= -25)
                _animator.SetTrigger(DamageMidHash);
            else
                _animator.SetTrigger(DamageHardHash);
        }
    }

    private void OnEnable() => _health.OnValueChanged += HpChanged;
    private void OnDisable() => _health.OnValueChanged -= HpChanged;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponentInChildren<HealthResource>();
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }
#endif
}
