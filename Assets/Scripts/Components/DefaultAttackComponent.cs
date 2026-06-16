using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DefaultAttackComponent : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public List<EffectAsset> Effects { get; private set; }

    public HashSet<EntityController> TargetsInRange { get; } = new();
    public HashSet<EntityController> DamagedTargets { get; } = new();

    protected EntityController entityController;

    protected float timeSinceLastAttack;

    public Action OnAttackStarted;
    public Action<EntityController> OnAttackApplied;

    protected virtual void Awake()
    {
        entityController = GetComponentInParent<EntityController>();

        OnAttackStarted += () => entityController.Animator.SetTrigger(AttackHash);
        OnAttackApplied += ApplyKnockback;
    }

    void ApplyKnockback(EntityController target)
    {
        Vector2 attackDir = (target.transform.position - entityController.transform.position).normalized;

        target.MovementBase.externalVelocityComponent.Add(attackDir * KnockbackPower);
    }

    public void UpdateAttackExecution()
    {
        foreach (var entityController in TargetsInRange.ToArray())
        {
            if (DamagedTargets.Contains(entityController) || entityController.IsDead)
                continue;

            DamagedTargets.Add(entityController);
            entityController.HealthComponent.Change(this.entityController.gameObject, -DealtDamage);

            OnAttackApplied?.Invoke(entityController);
        }
    }

    public void StartAttackExecution()
    {
        DamagedTargets.Clear();
    }
}
