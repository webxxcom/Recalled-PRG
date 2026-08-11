using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthProvider : ValueProvider<DamageInfo>
{
    [Header("Prefabs")]
    [SerializeField] ParticleSystem _damageParticles;
    [SerializeField] PopupDamageText _damagePopup;
    public Collider2D Hurtbox { get; private set; }
    public EffectMachineSO EffectMachine { get; private set; }
    public bool IsInvincible { get; set; }

    private void Awake()
    {
        EffectMachine = ScriptableObject.CreateInstance<EffectMachineSO>();
        Hurtbox = GetComponent<Collider2D>();
        Init();
    }

    private void OnEnable()
    {
        OnValueChanged += Particles;
        OnValueChanged += ApplyKnockback;
        OnValueChanged += PopupDamage;
    }

    private void OnDisable()
    {
        OnValueChanged -= Particles;
        OnValueChanged -= ApplyKnockback;
        OnValueChanged -= PopupDamage;
    }

    void Particles(DamageInfo di)
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, di.Direction);

        Instantiate(_damageParticles, di.Hurtbox.bounds.center, rot);
    }

    void ApplyKnockback(DamageInfo di)
    {
        MovementBase movementBase = di.Hurtbox.GetComponentInParent<MovementBase>();

        if (movementBase)
            movementBase.AddExternalVelocity(di.Direction * di.KnockbackPower);
    }

    void PopupDamage(DamageInfo di)
    {
        Instantiate(_damagePopup, Hurtbox.bounds.center, Quaternion.identity).Init(di.Amount + "");
    }

    public bool IsDead => CurrentValue <= 0;

    public void DealDamage(DamageInfo damageInfo)
    {
        if (IsInvincible)
            return;

        Change(damageInfo.Amount, damageInfo);
    }

}
