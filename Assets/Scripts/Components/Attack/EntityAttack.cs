using System;
using UnityEngine;

/// <summary>
/// EntityAttack component describes an object which has an animator and reload time.
/// Used for enemies and player
/// </summary
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class EntityAttack : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [field: SerializeField] public AttackSO AttackData { get; private set; }
    public CapsuleCollider2D Hitbox { get; private set; }


    protected EntityController _entityController;
    protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;

    protected virtual void Awake()
    {
        Hitbox = GetComponent<CapsuleCollider2D>();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
    }

    private void Start()
    {
        _timeSinceLastAttack = AttackData.ReloadTime;
        AttackData.KnockbackOriginTransform = transform;
    }

    protected void Attack()
    {
        _entityController.Animator.SetTrigger(AttackHash);

        OnAttackStarted?.Invoke();
    }
}
