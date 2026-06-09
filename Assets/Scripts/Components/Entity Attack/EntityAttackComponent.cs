using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IAttackStrategy))]
[RequireComponent(typeof(Collider2D))]
public class EntityAttackComponent : DefaultAttackComponent
{
    public PlayerController PlayerController { get; private set; }

    float timeSinceLastAttack;
    IAttackStrategy attackStrategy;
    new Collider2D collider2D;
    EntityMovementComponent entityMovementComponent;

    public event Action OnAttack;

    public void ExecuteAttack()
    {
        attackStrategy.Execute();
        Effects.ForEach(e => PlayerController.EffectMachineComponent.ApplyEffect(e));
    }

    private void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();
        collider2D = GetComponent<Collider2D>();

        entityMovementComponent = GetComponentInParent<EntityMovementComponent>();
    }

    private void Start()
    {
        timeSinceLastAttack = ReloadTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent hc))
        {
            PlayerController = hc.GetComponentInParent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Get Hitbox component and if PlayerController exists in parent then it's the player
        if (collision.TryGetComponent(out HitboxComponent hc) && hc.GetComponentInParent<PlayerController>())
        {
            PlayerController = null;
        }
    }

    void Attack()
    {
        timeSinceLastAttack = 0;

        OnAttack?.Invoke();
    }

    void SetAttackCollisionOffset()
    {
        if (!entityMovementComponent.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, entityMovementComponent.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime && PlayerController != null && !PlayerController.IsDead;
    private void Update()
    {
        if (CanAttack)
        {
            Attack();
        }

        timeSinceLastAttack += Time.deltaTime;
        SetAttackCollisionOffset();
    }
}
