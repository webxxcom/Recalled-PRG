using UnityEngine;

[RequireComponent(typeof(EntityMovementComponent))]
public class EnemyController : EntityController
{
    public EntityMovementComponent MovementComponent { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<EntityMovementComponent>();
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 finalMovement = MovementComponent.GetFinalMovement();

        if (finalMovement != Vector2.zero)
            Rigidbody2D.linearVelocity = finalMovement;

        Animator.SetFloat(MovementBase.MoveXHash, Mathf.Abs(MovementComponent.FacingDirection.x) > 0.01f ? MovementComponent.FacingDirection.x : 0f);
        Animator.SetFloat(MovementBase.MoveYHash, Mathf.Abs(MovementComponent.FacingDirection.x) < 0.01f ? MovementComponent.FacingDirection.y : 0f);
        Animator.SetFloat(MovementBase.SpeedHash, Rigidbody2D.linearVelocity.magnitude / 4f);
    }
}
