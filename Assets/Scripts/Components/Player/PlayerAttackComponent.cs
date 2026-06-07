using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackComponent : DefaultAttackComponent
{
    float timeSinceLastAttack;

    readonly HashSet<EntityController> targetsInRange = new();
    readonly HashSet<EntityController> damagedTargets = new();

    new Collider2D collider2D;
    PlayerMovementComponent playerMovementComponent;
    Animator animator;
    PlayerController playerController;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
        playerMovementComponent = GetComponentInParent<PlayerMovementComponent>();
        playerController = GetComponentInParent<PlayerController>();
    }

    public void UpdateAttackExecution()
    {
        foreach (var entityController in targetsInRange.ToArray())
        {
            if (damagedTargets.Contains(entityController) || entityController.IsDead)
                continue;

            damagedTargets.Add(entityController);
            entityController.GetComponent<HealthComponent>().Change(playerController.gameObject, -DealtDamage);
        }
    }

    public void StartAttackExecution()
    {
        damagedTargets.Clear();
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime;
    void OnAttack(InputValue value)
    {
        if (value.isPressed && CanAttack)
        {
            timeSinceLastAttack = 0;
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            EntityController entityController = collision.GetComponentInParent<EntityController>();

            if (entityController && !entityController.IsDead)
                targetsInRange.Add(entityController);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            targetsInRange.Remove(collision.GetComponentInParent<EntityController>());
        }
    }

    void SetAttackCollisionOffset()
    {
        if (!playerMovementComponent.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, playerMovementComponent.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        SetAttackCollisionOffset();
        timeSinceLastAttack += Time.deltaTime;
    }
}
