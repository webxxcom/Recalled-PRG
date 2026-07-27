using UnityEngine;

public class TrainingDummyController : MonoBehaviour
{
    private static readonly int DamageHardHash = Animator.StringToHash("HurtHard");
    private static readonly int DamageMidHash = Animator.StringToHash("HurtMid");
    private static readonly int DamageLightHash = Animator.StringToHash("HurtLight");

    [SerializeField] HealthProvider _healthProvider;
    [SerializeField] Animator _animator;

    void OnHurt(DamageInfo damageInfo)
    {
        if (damageInfo.Amount < 0)
        {
            if (damageInfo.Amount >= -10)
                _animator.SetTrigger(DamageLightHash);
            else if (damageInfo.Amount >= -25)
                _animator.SetTrigger(DamageMidHash);
            else
                _animator.SetTrigger(DamageHardHash);
        }
    }

    private void OnEnable() => _healthProvider.OnHpChanged += OnHurt;
    private void OnDisable() => _healthProvider.OnHpChanged -= OnHurt;
}
