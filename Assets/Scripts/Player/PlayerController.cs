using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Invincibility))]
[RequireComponent(typeof(BlinkingEffect))]
[RequireComponent(typeof(HealthProvider))]
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
    [SerializeField] DamageInfoGameEvent OnHpChanged;
    [SerializeField] DamageInfoGameEvent OnDeath;

    PlayerMovement _movementComponent;
    Invincibility _invincibility;
    BlinkingEffect _blinkingEffect;
    HealthProvider _healthProvider;

    protected override void Awake()
    {
        base.Awake();

        _movementComponent = GetComponent<PlayerMovement>();
        _invincibility = GetComponent<Invincibility>();
        _blinkingEffect = GetComponent<BlinkingEffect>();
        _healthProvider = GetComponent<HealthProvider>();

        IsArmed = true;
    }

    void OnEnable()
    {
        _healthProvider.OnHpChanged += HandleOnHpChangedGameEvent;
        _healthProvider.OnMinHpReached += HandleOnDeathGameEvent;
    }

    void OnDisable()
    {
        _healthProvider.OnHpChanged -= HandleOnHpChangedGameEvent;
        _healthProvider.OnMinHpReached -= HandleOnDeathGameEvent;
    }

    void HandleOnHpChangedGameEvent(DamageInfo damageInfo) => OnHpChanged.Raise(damageInfo);
    void HandleOnDeathGameEvent(DamageInfo damageInfo) => OnDeath.Raise(damageInfo);
}
