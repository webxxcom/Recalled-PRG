using UnityEngine;
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(ChaseComponent))]
public class ArcherController : EntityController
{
    ChaseComponent chaseComponent;
    EnemyAttackComponent enemyAttackComponent;
    Vector2 movement;

    void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    protected override void Awake()
    {
        base.Awake();

        chaseComponent = GetComponent<ChaseComponent>();
        enemyAttackComponent = GetComponentInChildren<EnemyAttackComponent>();
    }

    protected override void Start()
    {
        enemyAttackComponent.OnAttack += PlayAttackAnimation;
    }

    private void Move(Vector2 dir)
    {
        movement = ApplyEnvironmentMovement(dir * currentSpeed);

        rb.linearVelocity = movement;
        HandleSpriteFlip(movement);
    }

    protected override void HandleFixedUpdate()
    {
        Move(chaseComponent.GetDirection());

        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }

    private void Update()
    {
    }
}
