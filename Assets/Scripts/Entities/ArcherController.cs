using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ChaseComponent))]
[RequireComponent(typeof(AgressionComponent))]
[RequireComponent(typeof(ChaseZoneComponent))]
public class ArcherController : EntityController
{
    ChaseComponent chaseComponent;
    ChaseZoneComponent chaseZoneComponent;
    AgressionComponent agressionComponent;
    EntityAttackComponent entityAttackComponent;
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
        chaseZoneComponent = GetComponent<ChaseZoneComponent>();
    }

    protected override void Start()
    {
        entityAttackComponent.OnAttack += PlayAttackAnimation;
        healthComponent.OnDamageTaken += agressionComponent.BecomeAgressive;
    }

    protected override void HandleFixedUpdate()
    {
        //Vector2 movement = entityMovementComponent.GetFinalMovement();

        //rb.linearVelocity = movement;
        //HandleSpriteFlip(movement);
        //animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }
}
