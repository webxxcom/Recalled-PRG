using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovementComponent))]
public class PlayerController : EntityController, ITargetable
{
    private static readonly int IsArmedHash = Animator.StringToHash("IsArmed");

    public bool IsArmed
    {
        get => m_isArmed;
        
        private set
        {
            m_isArmed = value;
            animator.SetBool(IsArmedHash, value);
        }
    }

    bool m_isArmed;

    PlayerMovementComponent playerMovementComponent;

    protected override void Awake()
    {
        base.Awake();

        playerMovementComponent = GetComponent<PlayerMovementComponent>();
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
            Vector2 movement = playerMovementComponent.FinalMovement;

            rigidbody2D.linearVelocity = movement;
        }
        else
        {
            rigidbody2D.linearVelocity *= 0.9f;
        }

        animator.SetFloat("Speed", rigidbody2D.linearVelocity.magnitude / playerMovementComponent.SprintingSpeed);
        animator.SetFloat("MoveX", playerMovementComponent.FacingDirection.x);
        animator.SetFloat("MoveY", playerMovementComponent.FacingDirection.y);
    }
}
