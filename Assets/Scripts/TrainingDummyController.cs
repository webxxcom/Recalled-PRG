using UnityEngine;

[RequireComponent(typeof(HealthProvider))]
public class TrainingDummyController : MonoBehaviour
{
    private static readonly int DamageHardHash = Animator.StringToHash("HurtHard");
    private static readonly int DamageMidHash = Animator.StringToHash("HurtMid");
    private static readonly int DamageLightHash = Animator.StringToHash("HurtLight");

    Animator _animator;
    HealthProvider _healthProvider;


    private void Awake()
    {
        _animator = Utils.GetComponentInChildrenIfNotPresent<Animator>(gameObject);
        _healthProvider = GetComponent<HealthProvider>();
    }

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

    //TODO
    //private void OnEnable() => _healthProvider.OnValueChanged += OnHurt;
    //private void OnDisable() => _healthProvider.OnValueChanged -= OnHurt;
}
