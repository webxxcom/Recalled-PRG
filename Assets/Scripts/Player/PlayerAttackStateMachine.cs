using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    EntityController _entityController;
    EntityAttack _entityAttack;
    MovementBase _movementBase;
    AttackSO _attackData;

    readonly List<Collider2D> _damagedTargets = new();
    readonly List<Collider2D> _hits = new(10);

    void CacheAll(Animator animator)
    {
        if (!_entityController)
            _entityController = animator.GetComponentInParent<EntityController>();

        if (!_movementBase)
            _movementBase = _entityController.GetComponent<MovementBase>();

        if (!_entityAttack)
            _entityAttack = _entityController.GetComponentInChildren<EntityAttack>();
        _attackData = _entityAttack.AttackData;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CacheAll(animator);
        _movementBase.SpeedAggregator.Add(_attackData.SpeedMultiplier);
        _damagedTargets.Clear();
    }

    void AdjustHitbox(float normalizedTime)
    {
        if (!_attackData.Curves)
            return;

        _entityAttack.Hitbox.size = new(
                _attackData.Curves.ColliderSizeX.length > 1
                ? _attackData.Curves.ColliderSizeX.Evaluate(normalizedTime)
                : _entityAttack.Hitbox.size.x,
                _attackData.Curves.ColliderSizeY.length > 1
                ? _attackData.Curves.ColliderSizeY.Evaluate(normalizedTime)
                : _entityAttack.Hitbox.size.y
                );
        _entityAttack.Hitbox.offset = new(
            _attackData.Curves.ColliderOffsetX.length > 1
            ? _attackData.Curves.ColliderOffsetX.Evaluate(normalizedTime)
            : _entityAttack.Hitbox.offset.x,
              _attackData.Curves.ColliderOffsetY.length > 1
            ? _attackData.Curves.ColliderOffsetY.Evaluate(normalizedTime)
            : _entityAttack.Hitbox.offset.y
            );
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < _attackData.ImpactTime || stateInfo.normalizedTime > _attackData.RecoveryTime)
            return;

        AdjustHitbox(stateInfo.normalizedTime);
        _hits.Clear();
        _entityAttack.Hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_damagedTargets.Contains(hit) || hit.CompareTag(_entityAttack.tag))
                continue;

            _damagedTargets.Add(hit);

            _attackData.DealDamage(_entityController.gameObject, _entityAttack.Hitbox, hit);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _movementBase.SpeedAggregator.Remove(_attackData.SpeedMultiplier);
        _damagedTargets.Clear();
    }
}
