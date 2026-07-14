using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(InvincibilityComponent))]
[RequireComponent(typeof(EffectMachineComponent))]
[RequireComponent(typeof(PlayerInventory))]
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

    public EffectMachineComponent EffectMachineComponent { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerMovement MovementComponent { get; private set; }
    public PlayerInteraction InteractionComponent { get; private set; }
    public InvincibilityComponent InvincibilityComponent { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<PlayerMovement>();
        InvincibilityComponent = GetComponent<InvincibilityComponent>();
        EffectMachineComponent = GetComponent<EffectMachineComponent>();
        Inventory =  GetComponent<PlayerInventory>();
        InteractionComponent = GetComponentInChildren<PlayerInteraction>();
    }

    protected override void Start()
    {
        base.Start();

        
    }

    void Invinsibility(GameObject _, int _2) => InvincibilityComponent.BecomeInvinsibleFor(1f);

    private void OnEnable()
    {
        HealthComponent.OnValueChanged += Invinsibility;
    }

    private void OnDisable()
    {
        HealthComponent.OnValueChanged -= Invinsibility;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
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
        Animator.SetFloat(MovementBase.SpeedHash, Rigidbody2D.linearVelocity.magnitude / MovementComponent.WalkingSpeed);
    }
    
    // Input layer
    void OnInteract(InputValue _) => InteractionComponent.InteractWithCurrent();
}
