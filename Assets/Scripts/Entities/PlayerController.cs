using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerMovementComponent))]

[RequireComponent(typeof(InvincibilityComponent))]
[RequireComponent(typeof(EffectMachineComponent))]
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

    PlayerMovementComponent playerMovementComponent;
    InteractionComponent interactionComponent;
    InvincibilityComponent invincibilityComponent;

    public readonly List<KeyDefinition> inventory = new();

    public void AddKey(KeyDefinition keyDefinition) => inventory.Add(keyDefinition);
    public void RemoveKey(KeyDefinition keyDefinition) => inventory.Remove(keyDefinition);
    public int ChestsUnlocked { get; set; } = 0;

    protected override void Awake()
    {
        base.Awake();

        playerMovementComponent = GetComponent<PlayerMovementComponent>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
        EffectMachineComponent = GetComponent<EffectMachineComponent>();
        interactionComponent = GetComponentInChildren<InteractionComponent>();
        IsArmed = true;
    }

    protected override void Start()
    {
        base.Start();

        //HealthComponent.OnValueChanged += (_, _) => invincibilityComponent.BecomeInvinsibleFor(1f);
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
        if (MovementBase.MovementBlocked)
            return;

        if (playerMovementComponent.IsWalking)
        {
            Vector2 movement = playerMovementComponent.GetFinalMovement();

            Rigidbody2D.linearVelocity = movement;
        }
        else
        {
            Rigidbody2D.linearVelocity *= 0.9f;
        }
    }
    
    // Input layer
    void OnInteract(InputValue _) => interactionComponent.InteractWithCurrent();
}
