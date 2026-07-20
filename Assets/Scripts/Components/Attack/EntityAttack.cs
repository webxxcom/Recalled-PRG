using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class EntityAttack : DefaultAttack
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [field: SerializeField] public float ReloadTime { get; private set; }

    public CapsuleCollider2D Hitbox { get; private set; }

    protected EntityController _entityController;
    protected float timeSinceLastAttack;

    public Action OnAttackStarted;

    protected override void Awake()
    {
        base.Awake();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
        Hitbox = GetComponent<CapsuleCollider2D>();

        knockbackOriginPosition = _entityController.transform;
        OnAttackStarted += () => _entityController.Animator.SetTrigger(AttackHash);
    }
}
