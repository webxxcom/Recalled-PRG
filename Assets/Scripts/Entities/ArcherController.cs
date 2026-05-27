using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherController : EntityController, IChaser
{
    [SerializeField] GameObject currentTarget;

    ChaseComponent chase;
    EnemyAttackComponent enemyAttackComponent;
    Vector2 movement;

    GameObject IChaser.CurrentTarget { get => currentTarget; set => currentTarget = value; }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    protected override void Awake()
    {
        base.Awake();
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
    }
}
