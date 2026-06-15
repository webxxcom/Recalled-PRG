using UnityEngine;

public class EnemyController : EntityController
{
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

    void ProcessAttackToTargetsWithinRange() => entityAttackComponent.ExecuteAttack();
    
    protected override void Start()
    {
        base.Start();

        HealthComponent.OnMinValueReached += (_) => DeactivateChildrenOnDeath();
        HealthComponent.OnValueChanged += (_, _) => canvasHiderScript.ShowCanvas();
    }

    protected override void HandleFixedUpdate()
    {
        if (MovementBase.MovementBlocked)
            return;

        Vector2 finalMovement = MovementBase.GetFinalMovement();

        if (finalMovement != Vector2.zero)
            Rigidbody2D.linearVelocity = finalMovement;
    }
}
