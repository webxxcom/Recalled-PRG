using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovementComponent))]
[RequireComponent(typeof(PlayerAttackComponent))]
public class PlayerController : EntityController, ITargetable
{
    PlayerMovementComponent movementComponent;
    PlayerAttackComponent playerAttackComponent;

    protected override void Awake()
    {
        base.Awake();

        movementComponent = GetComponent<PlayerMovementComponent>();
        playerAttackComponent = GetComponent<PlayerAttackComponent>();
    }

    void OnAttack(InputValue value)
    {
        healthComponent.Die();
        animator.SetTrigger("PlayDeath");
        IsDead = true;
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
        if (movementComponent.IsWalking)
        {
            Vector2 movement = movementComponent.GetFinalMovement();

            rb.linearVelocity = movement;
        }
        else
        {
            rb.linearVelocity *= 0.9f;
        }

        animator.SetFloat("Speed", rb.linearVelocity.magnitude / movementComponent.SprintingSpeed);
        animator.SetFloat("MoveX", rb.linearVelocityX);
        animator.SetFloat("MoveY", rb.linearVelocityY);
    }

    void Update()
    {
    }
}
