using NUnit.Framework;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    EntityController _entityController;
    EntityAttack _entityAttack;
    MovementBase _movementBase;
    AttackData attackData;

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
        attackData = _entityAttack.AttackData;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CacheAll(animator);
        _movementBase.SpeedAggregator.Add(attackData.SpeedMultiplier);
        _damagedTargets.Clear();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < attackData.ImpactTime || stateInfo.normalizedTime > attackData.RecoveryTime)
            return;

        if (attackData.ColliderSizeX.length != 0 || attackData.ColliderSizeY.length != 0)
        {
            _entityAttack.Hitbox.size = new(
                attackData.ColliderSizeX.Evaluate(stateInfo.normalizedTime),
                attackData.ColliderSizeY.Evaluate(stateInfo.normalizedTime)
                );
        }
        if (attackData.ColliderOffsetX.length != 0 || attackData.ColliderOffsetY.length != 0)
        {
            _entityAttack.Hitbox.offset = new(
            attackData.ColliderOffsetX.Evaluate(stateInfo.normalizedTime),
            attackData.ColliderOffsetY.Evaluate(stateInfo.normalizedTime)
            );
        }

        _hits.Clear();
        _entityAttack.Hitbox.Overlap(_hits);
        foreach (Collider2D hit in _hits)
        {
            if (_damagedTargets.Contains(hit) || !_entityAttack.HostileFactions.Contains(hit.GetComponentInParent<FactionComponent>().Faction))
                continue;

            _damagedTargets.Add(hit);

            _entityAttack.DealDamage(hit.GetComponentInParent<HealthProvider>(), _entityAttack.gameObject);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _movementBase.SpeedAggregator.Remove(attackData.SpeedMultiplier);
        _damagedTargets.Clear();
    }
}
