using UnityEngine;

public class PlayerController : EntityController
{
    private static readonly int IsArmedHash = Animator.StringToHash("IsArmed");

    bool _isArmed;
    public bool IsArmed
    {
        get => _isArmed;
        private set
        {
            _isArmed = value;
            Animator.SetBool(IsArmedHash, value);
        }
    }

    [Header("Broadcasts to")]
    [SerializeField] DamageInfoGameEvent OnHpChangedChannel;
    [SerializeField] DamageInfoGameEvent OnDeathChannel;

    HealthProvider _healthProvider;

    protected override void Awake()
    {
        base.Awake();

        _healthProvider = GetComponentInChildren<HealthProvider>();

        IsArmed = true;
    }

    void OnEnable()
    {
        _healthProvider.OnValueChanged += HandleOnHpChangedGameEvent;
        _healthProvider.OnMinValue += HandleOnDeathGameEvent;
    }

    void OnDisable()
    {
        _healthProvider.OnValueChanged -= HandleOnHpChangedGameEvent;
        _healthProvider.OnMinValue -= HandleOnDeathGameEvent;
    }

    void HandleOnHpChangedGameEvent(DamageInfo damageInfo) => OnHpChangedChannel.Raise(damageInfo);
    void HandleOnDeathGameEvent(DamageInfo damageInfo) => OnDeathChannel.Raise(damageInfo);
}
