using UnityEngine;

public class EnemyController : EntityController
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    EntityAttackComponent entityAttackComponent;
    HitboxComponent hitboxComponent;
    CanvasHiderScript canvasHiderScript;

    protected override void Awake()
    {
        base.Awake();

        entityAttackComponent = GetComponentInChildren<EntityAttackComponent>();
        hitboxComponent = GetComponentInChildren<HitboxComponent>();
        canvasHiderScript = GetComponentInChildren<CanvasHiderScript>();
    }

    void DeactivateChildrenOnDeath()
    {
        entityAttackComponent.gameObject.SetActive(false);
        hitboxComponent.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        entityAttackComponent.OnAttack += () => animator.SetTrigger(AttackHash);
        healthComponent.OnMinValueReached += (_) => DeactivateChildrenOnDeath();
        healthComponent.OnValueChanged += (_, _) => canvasHiderScript.ShowHealthBar();
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 finalMovement = MovementBase.GetFinalMovement();

        if (finalMovement != Vector2.zero)
            rigidbody2D.linearVelocity = finalMovement;
    }
}
