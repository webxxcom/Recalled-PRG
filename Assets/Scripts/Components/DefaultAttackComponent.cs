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

    public Action OnAttackEvent;

    protected virtual void Awake()
    {
        entityController = GetComponentInParent<EntityController>();

        OnAttackEvent += () => entityController.Animator.SetTrigger(AttackHash);
    }

    public void UpdateAttackExecution()
    {
        foreach (var entityController in TargetsInRange.ToArray())
        {
            if (DamagedTargets.Contains(entityController) || entityController.IsDead)
                continue;

            DamagedTargets.Add(entityController);
            entityController.HealthComponent.Change(this.entityController.gameObject, -DealtDamage);
        }
    }

    public void StartAttackExecution()
    {
        DamagedTargets.Clear();
    }
}
