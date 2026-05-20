using UnityEngine;

[RequireComponent(typeof(ChaseComponent))]
public class ArcherController : EntityController
{
    GameObject player;
    ChaseComponent chase;
    Vector2 movement;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
        chase = GetComponent<ChaseComponent>();
    }

    private void Move(Vector2 dir)
    {
        movement = ApplyEnvironmentMovement(dir * currentSpeed);

        rb.linearVelocity = movement;
        HandleSpriteFlip(movement);
    }

    private void FixedUpdate()
    {
        Move(chase.GetDirection());
        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            chase.Target = collision.transform;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.CompareTag("Player"))
        {
            chase.Target = null;
        }
    }
}
