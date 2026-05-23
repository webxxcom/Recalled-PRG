using Mono.Cecil.Cil;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class ArcherController : EntityController
{
    ChaseComponent chase;
    EnemyAttackComponent enemyAttackComponent;
    ProjectileAttackComponent projectileAttackComponent;
    Vector2 movement;

    void SpawnArrow() => projectileAttackComponent.SpawnProjectile();

    void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    protected override void Awake()
    {
        base.Awake();
        projectileAttackComponent = GetComponentInChildren<ProjectileAttackComponent>();
        enemyAttackComponent = GetComponentInChildren<EnemyAttackComponent>();

        TryGetComponent(out chase);
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
        if (chase)
            Move(chase.GetDirection());

        animator.SetBool("isWalking", movement.sqrMagnitude > 0.01f);
    }

    private void Update()
    {
        Debug.Log(rb.linearVelocity);
    }
}
