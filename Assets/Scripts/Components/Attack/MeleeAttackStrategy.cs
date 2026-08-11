using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class MeleeAttackStrategy : AttackStrategy
{
    [SerializeField] MeleeAttackSO _meleeAttackData;
    public override AttackSO AttackData => _meleeAttackData;
    public override int AnimatorHash => AnimatorParameters.MeleeHash;
    protected override bool WithinAttackRange { get; set; }

    CapsuleCollider2D _hitbox;
    MovementBase _movementBase;

    private void Awake()
    {
        _hitbox = GetComponent<CapsuleCollider2D>();
        _movementBase = GetComponentInParent<MovementBase>();
    }

    private void OnEnable()
    {
        _movementBase.OnMovement += SetAttackCollisionOffset;
    }
    private void OnDisable()
    {
        _movementBase.OnMovement -= SetAttackCollisionOffset;
    }

    void SetAttackCollisionOffset()
    {
        _hitbox.transform.rotation
            = Quaternion.FromToRotation(Vector2.right, _movementBase.MovementIntention);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            WithinAttackRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            WithinAttackRange = false;
    }

    readonly List<Collider2D> _damagedTargets = new();
    readonly List<Collider2D> _hits = new(10);

    public override void StartExecuting()
    {
        _elapsedSinceAttack = 0;
    }

    public override void ProcessState(float normalizedTime)
    {
        if (normalizedTime < _meleeAttackData.ImpactTime || normalizedTime > _meleeAttackData.RecoveryTime)
            return;

        _meleeAttackData.HitboxOverTime(_hitbox, normalizedTime);
        _hits.Clear();
        _hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_damagedTargets.Contains(hit) || hit.CompareTag(_hitbox.tag))
                continue;

            _damagedTargets.Add(hit);

            _meleeAttackData.DealDamage(transform.parent.gameObject, _hitbox, hit);
        }
    }

    public override void FinishExecuting()
    {
        _damagedTargets.Clear();
    }
}
