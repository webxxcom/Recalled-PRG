using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    EntityController _entityController;
    EntityAttack _entityAttack;
    MovementBase _movementBase;
    AttackDataSO _attackData;

    readonly List<Collider2D> _damagedTargets = new();
    readonly List<Collider2D> _hits = new(4);

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

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < _attackData.ImpactTime || stateInfo.normalizedTime > _attackData.RecoveryTime)
            return;

        if (_attackData.Curves)
        {
            _entityAttack.Hitbox.size = new(
                _attackData.Curves.ColliderSizeX.Evaluate(stateInfo.normalizedTime),
                _attackData.Curves.ColliderSizeY.Evaluate(stateInfo.normalizedTime)
                );
            _entityAttack.Hitbox.offset = new(
                _attackData.Curves.ColliderOffsetX.Evaluate(stateInfo.normalizedTime),
                _attackData.Curves.ColliderOffsetY.Evaluate(stateInfo.normalizedTime)
                );
        }

        _hits.Clear();
        _entityAttack.Hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_damagedTargets.Contains(hit) || hit.CompareTag(_entityAttack.tag))
                continue;

            _damagedTargets.Add(hit);

            _entityAttack.AttackData.DealDamage(hit.GetComponentInParent<HealthProvider>(), _entityAttack.gameObject);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _movementBase.SpeedAggregator.Remove(_attackData.SpeedMultiplier);
        _damagedTargets.Clear();
    }
}
