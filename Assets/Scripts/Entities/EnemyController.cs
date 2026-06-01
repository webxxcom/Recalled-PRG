using UnityEngine;

public class EnemyController : EntityController
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");

    EntityAttackComponent entityAttackComponent;
    HitboxComponent hitboxComponent;

    protected override void Awake()
    {
        base.Awake();

        entityAttackComponent = GetComponentInChildren<EntityAttackComponent>();
        hitboxComponent = GetComponentInChildren<HitboxComponent>();
    }

    void DeactivateChildrenOnDeath()
    {
        entityAttackComponent.gameObject.SetActive(false);
        hitboxComponent.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        healthComponent.OnValueChanged += (_, _) => animator.SetTrigger(HurtHash);
        entityAttackComponent.OnAttack += () => animator.SetTrigger(AttackHash);
        healthComponent.OnMinValueReached += (_) => DeactivateChildrenOnDeath();
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 finalMovement = MovementBase.GetFinalMovement();

        if (finalMovement != Vector2.zero)
            rigidbody2D.linearVelocity = finalMovement;
    }
}
