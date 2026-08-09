using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class MeleeAttackStrategy : AttackStrategy
{
    [field: SerializeField] public MeleeAttackSO MeleeAttackSO { get; protected set; }
    public override AttackSO AttackData => MeleeAttackSO;
    public CapsuleCollider2D Hitbox { get; private set; }
    protected override bool WithinAttackRange { get; set; }
    public override int AnimatorHash => AnimatorParameters.MeleeHash;

    MovementBase _movementBase;

    private void Awake()
    {
        Hitbox = GetComponent<CapsuleCollider2D>();
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
        Hitbox.transform.rotation
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

    void AdjustHitbox(float normalizedTime)
    {
        if (!MeleeAttackSO.Curves)
            return;

        Hitbox.size = new(
                MeleeAttackSO.Curves.ColliderSizeX.length > 1
                ? MeleeAttackSO.Curves.ColliderSizeX.Evaluate(normalizedTime)
                : Hitbox.size.x,
                MeleeAttackSO.Curves.ColliderSizeY.length > 1
                ? MeleeAttackSO.Curves.ColliderSizeY.Evaluate(normalizedTime)
                : Hitbox.size.y
                );
        Hitbox.offset = new(
            MeleeAttackSO.Curves.ColliderOffsetX.length > 1
            ? MeleeAttackSO.Curves.ColliderOffsetX.Evaluate(normalizedTime)
            : Hitbox.offset.x,
              MeleeAttackSO.Curves.ColliderOffsetY.length > 1
            ? MeleeAttackSO.Curves.ColliderOffsetY.Evaluate(normalizedTime)
            : Hitbox.offset.y
            );
    }

    public override void ProcessState(float normalizedTime)
    {
        if (normalizedTime < MeleeAttackSO.ImpactTime || normalizedTime > MeleeAttackSO.RecoveryTime)
            return;

        AdjustHitbox(normalizedTime);
        _hits.Clear();
        Hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_damagedTargets.Contains(hit) || hit.CompareTag(Hitbox.tag))
                continue;

            _damagedTargets.Add(hit);

            MeleeAttackSO.DealDamage(transform.parent.gameObject, Hitbox, hit);
        }
    }

    public override void FinishExecuting()
    {
        _damagedTargets.Clear();
    }
}
