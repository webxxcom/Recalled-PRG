using CsvHelper.Configuration.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(InvincibilityProvider))]
[RequireComponent(typeof(EffectMachine))]
[RequireComponent(typeof(MovementBase))]
public class PlayerController : EntityController
{
    private static readonly int IsArmedHash = Animator.StringToHash("IsArmed");

    public bool IsArmed
    {
        get => _isArmed;
        
        private set
        {
            _isArmed = value;
            Animator.SetBool(IsArmedHash, value);
        }
    }

    bool _isArmed;

    [Header("Broadcasts to")]
    [SerializeField] GameobjectIntGameEvent OnHpChanged;
    [SerializeField] GameobjectGameEvent OnDeath;

    public EffectMachine EffectMachineComponent { get; private set; }
    public PlayerMovement MovementComponent { get; private set; }
    public PlayerInteraction InteractionComponent { get; private set; }
    public InvincibilityProvider InvincibilityComponent { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<PlayerMovement>();
        InvincibilityComponent = GetComponent<InvincibilityProvider>();
        EffectMachineComponent = GetComponent<EffectMachine>();
        InteractionComponent = Utils.FindOrThrow(GetComponentInChildren<PlayerInteraction>);

        IsArmed = true;
    }

    void Invinsibility(GameObject _, int _2) => InvincibilityComponent.BecomeInvinsibleFor(1f);

    protected override void OnEnable()
    {
        base.OnEnable();

        HealthProvider.Health.OnValueChanged += Invinsibility;
        HealthProvider.Health.OnValueChanged += HandleOnHpValueChanged;
        HealthProvider.Health.OnMinValueReached += HandleOnMinHpReached;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        HealthProvider.Health.OnValueChanged -= Invinsibility;
        HealthProvider.Health.OnValueChanged -= HandleOnHpValueChanged;
        HealthProvider.Health.OnMinValueReached -= HandleOnMinHpReached;
    }

    void HandleOnHpValueChanged(GameObject changer, int val) => OnHpChanged.Invoke(changer, val);
    void HandleOnMinHpReached(GameObject changer) => OnDeath.Invoke(changer);

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 movement = MovementComponent.GetFinalMovement(); 
        
        if (movement != Vector2.zero)
        {
            Rigidbody2D.linearVelocity = movement;
        }
        else
        {
            Rigidbody2D.linearVelocity *= 0.9f;
        }

        Animator.SetFloat(MovementBase.MoveXHash, MovementComponent.FacingDirection.x);
        Animator.SetFloat(MovementBase.MoveYHash, MovementComponent.FacingDirection.y);
        Animator.SetFloat(MovementBase.SpeedHash,
            Rigidbody2D.linearVelocity.magnitude / (MovementComponent.WalkingSpeed * MovementComponent.SprintingSpeedMultiplier));
    }
    
    // Input layer
    void OnInteract(InputValue _) => InteractionComponent.InteractWithCurrent();
}
