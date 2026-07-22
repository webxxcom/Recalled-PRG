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

    [SerializeField] HealthProvider _healthProvider;

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<EntityMovementComponent>();
        InvincibilityProvider = GetComponent<InvincibilityProvider>();
        entityAttackComponent = GetComponentInChildren<EnemyAttack>();
        hitboxComponent = GetComponentInChildren<Hurtbox>();
        canvasHiderScript = GetComponentInChildren<CanvasHider>();
    }

    void Invinsibility(GameObject _, int val)
    { if (val < 0) InvincibilityProvider.BecomeInvinsibleFor(1f); }


    protected override void OnEnable()
    {
        base.OnEnable();

        _healthProvider.Health.OnValueChanged += Invinsibility;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _healthProvider.Health.OnValueChanged -= Invinsibility;
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
