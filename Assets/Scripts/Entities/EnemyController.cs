using UnityEngine;

[RequireComponent(typeof(MovementBase))]
[RequireComponent(typeof(EntityMovementComponent))]
[RequireComponent(typeof(InvincibilityProvider))]
public class EnemyController : EntityController
{
    EnemyAttack entityAttackComponent;
    Hurtbox hitboxComponent;
    CanvasHider canvasHiderScript;

    public EntityMovementComponent MovementComponent { get; private set; }
    public InvincibilityProvider InvincibilityProvider { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<EntityMovementComponent>();
        InvincibilityProvider = GetComponent<InvincibilityProvider>();
        entityAttackComponent = GetComponentInChildren<EnemyAttack>();
        hitboxComponent = GetComponentInChildren<Hurtbox>();
        canvasHiderScript = GetComponentInChildren<CanvasHider>();
    }

    void Invinsibility(GameObject _, int _2) => InvincibilityProvider.BecomeInvinsibleFor(1f);

    protected override void OnEnable()
    {
        base.OnEnable();

        Health.OnValueChanged += Invinsibility;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Health.OnValueChanged -= Invinsibility;
    }

    void DeactivateChildrenOnDeath()
    {
        entityAttackComponent.gameObject.SetActive(false);
        hitboxComponent.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        Health.OnMinValueReached += (_) => DeactivateChildrenOnDeath();
        Health.OnValueChanged += (_, _) => canvasHiderScript.ShowCanvas();
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
