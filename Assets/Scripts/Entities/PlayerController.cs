using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent))]
[RequireComponent(typeof(InvincibilityComponent))]
[RequireComponent(typeof(EffectMachineComponent))]
[RequireComponent(typeof(PlayerInventoryComponent))]
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
    public PlayerInventoryComponent Inventory { get; private set; }
    public PlayerMovementComponent MovementComponent { get; private set; }

    PlayerInteractionComponent interactionComponent;
    InvincibilityComponent invincibilityComponent;

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<PlayerMovementComponent>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
        EffectMachineComponent = GetComponent<EffectMachineComponent>();
        Inventory = GetComponent<PlayerInventoryComponent>();
        interactionComponent = GetComponentInChildren<PlayerInteractionComponent>();
        IsArmed = true;
    }

    protected override void Start()
    {
        base.Start();

        HealthComponent.OnValueChanged += (_, _) => invincibilityComponent.BecomeInvinsibleFor(1f);
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
    void OnInteract(InputValue _) => interactionComponent.InteractWithCurrent();
}
