using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EntityAttack : DefaultAttack
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [field: SerializeField] public float ReloadTime { get; private set; }

    public HashSet<EntityController> TargetsInRange { get; } = new();
    public HashSet<EntityController> DamagedTargets { get; } = new();

    protected EntityController entityController;
    protected float timeSinceLastAttack;

    public Action OnAttackStarted;

    protected override void Awake()
    {
        base.Awake();

        entityController = GetComponentInParent<EntityController>();
        knockbackOriginPosition = entityController.transform;
        OnAttackStarted += () => entityController.Animator.SetTrigger(AttackHash);
    }

    public void UpdateAttackExecution()
    {
        foreach (var entityController in TargetsInRange.ToArray())
        {
            if (DamagedTargets.Contains(entityController) || entityController.HealthComponent.IsDead)
                continue;

            DamagedTargets.Add(entityController);
            entityController.HealthComponent.Change(this.entityController.gameObject, -BasicDealtDamage);

            OnAttackApplied?.Invoke(entityController);
        }
    }

    public void StartAttackExecution()
    {
        DamagedTargets.Clear();
    }

    protected override void HandleOnTriggerEnter2D(Collider2D collision)
    {
        EntityController ec = collision.GetComponentInParent<EntityController>();

        if (ec)
            TargetsInRange.Add(ec);
    }
    protected override void HandleOnTriggerExit2D(Collider2D collision)
    {
        TargetsInRange.Remove(collision.GetComponentInParent<EntityController>());
    }
}
