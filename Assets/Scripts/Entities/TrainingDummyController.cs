using UnityEngine;

public class TrainingDummyController : MonoBehaviour
{
    private static readonly int DamageHardHash = Animator.StringToHash("HurtHard");
    private static readonly int DamageMidHash = Animator.StringToHash("HurtMid");
    private static readonly int DamageLightHash = Animator.StringToHash("HurtLight");

    [SerializeField] HealthProvider _healthProvider;
    [SerializeField] Animator _animator;

    void OnHurt(GameObject _, int val)
    {
        if (val < 0)
        {
            if (val >= -10)
                _animator.SetTrigger(DamageLightHash);
            else if (val >= -25)
                _animator.SetTrigger(DamageMidHash);
            else
                _animator.SetTrigger(DamageHardHash);
        }
    }

    private void OnEnable() => _healthProvider.Value.OnValueChanged += OnHurt;
    private void OnDisable() => _healthProvider.Value.OnValueChanged -= OnHurt;
}
