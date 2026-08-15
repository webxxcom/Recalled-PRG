using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class MeleeAttackStrategy : AttackStrategy
{
    [SerializeField] MeleeAttackSO _meleeAttackData;
    public override AttackSO AttackData => _meleeAttackData;
    public override int AnimatorHash => AnimatorParameters.MeleeHash;

    protected override bool WithinAttackRange(AttackContext attackContext)
    {
        HealthResource v = attackContext.Target.GetComponentInChildren<HealthResource>();

        return _hitbox.IsTouching(v.Hurtbox) && false;
    }

    CapsuleCollider2D _hitbox;
    MovementBase _movementBase;
    EntityController _entityController;

    private void Awake()
    {
        _hitbox = GetComponent<CapsuleCollider2D>();
        _entityController = GetComponentInParent<EntityController>();
        _movementBase = GetComponentInParent<MovementBase>();
    }

    private void OnEnable()
        => _movementBase.OnMovement += SetAttackCollisionOffset;
    private void OnDisable()
        => _movementBase.OnMovement -= SetAttackCollisionOffset;

    void SetAttackCollisionOffset()
    {
        _hitbox.transform.rotation
            = Quaternion.FromToRotation(Vector2.right, _movementBase.MovementIntention);
    }

    readonly List<Collider2D> _processedTargets = new();
    readonly List<Collider2D> _hits = new(10);

    public override void StartExecuting(AttackContext attackContext)
    {
        _elapsedSinceAttack = 0;
    }

    public override void ProcessState(float normalizedTime, AttackContext attackContext)
    {
        if (normalizedTime < _meleeAttackData.ImpactTime || normalizedTime > _meleeAttackData.RecoveryTime)
            return;

        _meleeAttackData.HitboxOverTime(_hitbox, normalizedTime);
        _hits.Clear();
        _hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_processedTargets.Contains(hit) || hit.CompareTag(_hitbox.tag))
                continue;

            _processedTargets.Add(hit);

            _meleeAttackData.DealDamage(_entityController, hit);
        }
    }

    public override void FinishExecuting(AttackContext attackContext)
    {
        _processedTargets.Clear();
    }
}
