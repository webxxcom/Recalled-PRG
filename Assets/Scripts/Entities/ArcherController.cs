using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ChaseComponent))]
[RequireComponent(typeof(AgressionComponent))]
[RequireComponent(typeof(EntityMovementComponent))]
public class ArcherController : EntityController
{
    ChaseComponent chaseComponent;
    AgressionComponent agressionComponent;
    EntityAttackComponent entityAttackComponent;
    EntityMovementComponent entityMovementComponent;
    Vector2 movement;

    void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    protected override void Awake()
    {
        base.Awake();

        chaseComponent = GetComponent<ChaseComponent>();
        agressionComponent = GetComponent<AgressionComponent>();
        entityAttackComponent = GetComponentInChildren<EntityAttackComponent>();
        entityMovementComponent = GetComponent<EntityMovementComponent>();
    }

    protected override void Start()
    {
        entityAttackComponent.OnAttack += PlayAttackAnimation;
        //healthComponent.OnValueChanged += agressionComponent.BecomeAgressive;
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 movement = entityMovementComponent.GetFinalMovement();

        rigidbody2D.linearVelocity = movement;
        HandleSpriteFlip(movement);
        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }
}
