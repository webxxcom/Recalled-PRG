using Mono.Cecil.Cil;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EntityMovementComponent))]
public class WarriorController : EntityController
{
    ChaseComponent chase;
    EntityAttackComponent entityAttackComponent;
    MeleeAttackComponent meleeAttackComponent;
    EntityMovementComponent entityMovementComponent;
    Vector2 movement;

    void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    protected override void Awake()
    {
        base.Awake();
        meleeAttackComponent = GetComponentInChildren<MeleeAttackComponent>();
        entityAttackComponent = GetComponentInChildren<EntityAttackComponent>();
        entityMovementComponent = GetComponent<EntityMovementComponent>();

        TryGetComponent(out chase);
    }

    protected override void Start()
    {
        entityAttackComponent.OnAttack += PlayAttackAnimation;
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 movement = entityMovementComponent.GetFinalMovement();

        rb.linearVelocity = movement;
        HandleSpriteFlip(movement);
        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }
}
