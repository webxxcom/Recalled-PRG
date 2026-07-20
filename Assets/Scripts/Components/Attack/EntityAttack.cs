using System;
using UnityEngine;

/// <summary>
/// EntityAttack component describes an object which has an animator and reload time.
/// Used for enemies and player
/// </summary>
public abstract class EntityAttack : DefaultAttack
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    protected EntityController _entityController;
    protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;

    protected override void Awake()
    {
        base.Awake();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
        Hitbox = Utils.GetComponentInChildrenIfNotPresent<CapsuleCollider2D>(gameObject);
    }

    private void Start()
    {
        _timeSinceLastAttack = AttackData.ReloadTime;
    }

    protected void Attack()
    {
        _entityController.Animator.SetTrigger(AttackHash);

        OnAttackStarted?.Invoke();
    }
}
