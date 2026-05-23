using UnityEngine;

public class ArcherController : EntityController
{
    ChaseComponent chase;
    Vector2 movement;

    protected override void Awake()
    {
        base.Awake();
        chase = GetComponent<ChaseComponent>();
    }

    private void Move(Vector2 dir)
    {
        if (IsFreezed)
            return;

        movement = ApplyEnvironmentMovement(dir * currentSpeed);

        rb.linearVelocity = movement;
        HandleSpriteFlip(movement);
    }

    private void FixedUpdate()
    {
        if (chase != null)
            Move(chase.GetDirection());
        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }
}
