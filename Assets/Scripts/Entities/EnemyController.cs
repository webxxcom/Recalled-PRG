using UnityEngine;

[RequireComponent(typeof(MovementBase))]
public class EnemyController : EntityController
{
    EntityAttackComponent entityAttackComponent;
    HitboxComponent hitboxComponent;
    CanvasHiderScript canvasHiderScript;

    public EntityMovementComponent MovementComponent { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<EntityMovementComponent>();
        entityAttackComponent = GetComponentInChildren<EntityAttackComponent>();
        hitboxComponent = GetComponentInChildren<HitboxComponent>();
        canvasHiderScript = GetComponentInChildren<CanvasHiderScript>();
    }

    void DeactivateChildrenOnDeath()
    {
        entityAttackComponent.gameObject.SetActive(false);
        hitboxComponent.gameObject.SetActive(false);
    }

    void ProcessAttackToTargetsWithinRange() => entityAttackComponent.ExecuteAttack();
    
    protected override void Start()
    {
        base.Start();

        HealthComponent.OnMinValueReached += (_) => DeactivateChildrenOnDeath();
        HealthComponent.OnValueChanged += (_, _) => canvasHiderScript.ShowCanvas();
    }

    protected override void HandleFixedUpdate()
    {
        Vector2 finalMovement = MovementComponent.GetFinalMovement();

        if (finalMovement != Vector2.zero)
            Rigidbody2D.linearVelocity = finalMovement;

        Animator.SetFloat(MovementBase.MoveXHash, Mathf.Abs(MovementComponent.FacingDirection.x) > 0.01f ? MovementComponent.FacingDirection.x : 0f);
        Animator.SetFloat(MovementBase.MoveYHash, Mathf.Abs(MovementComponent.FacingDirection.x) < 0.01f ? MovementComponent.FacingDirection.y : 0f);
        Animator.SetFloat(MovementBase.SpeedHash, Rigidbody2D.linearVelocity.magnitude / MovementComponent.WalkingSpeed);
    }
}
