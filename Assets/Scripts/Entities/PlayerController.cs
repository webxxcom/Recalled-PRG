using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent))]
public class PlayerController : EntityController, ITargetable
{
    private static readonly int IsArmedHash = Animator.StringToHash("IsArmed");

    public bool IsArmed
    {
        get => _isArmed;
        
        private set
        {
            _isArmed = value;
            animator.SetBool(IsArmedHash, value);
        }
    }

    bool _isArmed;

    PlayerMovementComponent playerMovementComponent;
    PlayerAttackComponent playerAttackComponent;
    InteractionComponent interactionComponent;

    public readonly List<KeyDefinition> inventory = new();

    public void AddKey(KeyDefinition keyDefinition) => inventory.Add(keyDefinition);
    public void RemoveKey(KeyDefinition keyDefinition) => inventory.Remove(keyDefinition);
    public int ChestsUnlocked { get; set; } = 0;


    protected override void Awake()
    {
        base.Awake();

        playerMovementComponent = GetComponent<PlayerMovementComponent>();
        interactionComponent = GetComponentInChildren<InteractionComponent>();
        playerAttackComponent = GetComponentInChildren<PlayerAttackComponent>();
        IsArmed = true;
    }

    protected override void Start()
    {
        base.Start();

        healthComponent.OnValueChanged += (_, _) => PlayHurtAnimation();
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
        if (playerMovementComponent.IsWalking)
        {
            Vector2 movement = playerMovementComponent.GetFinalMovement();

            rigidbody2D.linearVelocity = movement;
        }
        else
        {
            rigidbody2D.linearVelocity *= 0.9f;
        }
    }

    // Input layer
    void OnInteract(InputValue _) => interactionComponent.InteractWithCurrent();
}
