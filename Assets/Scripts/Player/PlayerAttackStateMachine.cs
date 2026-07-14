using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateMachine : StateMachineBehaviour
{
    [SerializeField] float impactTime = 0.3f;
    [SerializeField] float recoveryTime = 0.8f;
    [SerializeField] float speedMultiplier = 0.3f;

    EntityController entityController;
    EntityAttack _entityAttack;
    MovementBase movementBase;

    readonly List<Collider2D> _damagedTargets = new();

    void CacheAll(Animator animator)
    {
        if (!entityController)
            entityController = animator.GetComponentInParent<EntityController>();

        if (!movementBase)
            movementBase = entityController.GetComponent<MovementBase>();

        if (!_entityAttack)
            _entityAttack = entityController.GetComponentInChildren<EntityAttack>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CacheAll(animator);
        movementBase.SpeedAggregator.Add(speedMultiplier);
        _damagedTargets.Clear();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < impactTime || stateInfo.normalizedTime > recoveryTime)
            return;

        Vector2 pos = _entityAttack.Hitbox.transform.position;
        pos.x += _entityAttack.Hitbox.offset.x;
        pos.y += _entityAttack.Hitbox.offset.y;
        Collider2D[] hits = Physics2D.OverlapBoxAll(pos, _entityAttack.Hitbox.size, 0f, _entityAttack.Hitbox.includeLayers);

        foreach (Collider2D hit in hits)
        {
            if (_damagedTargets.Contains(hit) || !_entityAttack.HostileFactions.Contains(hit.GetComponentInParent<FactionComponent>().Faction))
                continue;

            _damagedTargets.Add(hit);

            HealthProvider hp = hit.GetComponentInParent<HealthProvider>();
            if (hp)
                _entityAttack.DealDamage(hp, _entityAttack.gameObject);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementBase.SpeedAggregator.Remove(speedMultiplier);
        _damagedTargets.Clear();
    }
}
