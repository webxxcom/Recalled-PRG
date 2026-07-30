using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityMovement : MovementBase
{
    [SerializeField] MovementAIConfig _movementAIConfig;

    readonly List<MovementStrategy> _movementStrategies = new();
    readonly List<TargetProvider> _targetProviders = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (var item in _movementAIConfig.MovementStrategies)
            _movementStrategies.Add(item.CreateInstance());

        foreach (var item in _movementAIConfig.TargetProviders)
            _targetProviders.Add(item.CreateInstance());
    }

    protected override Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        TargetProvider targetProvider = _targetProviders
            .FirstOrDefault(e => e.HasTarget);

        Vector2 dir = Vector2.zero;
        foreach (var item in _movementStrategies)
        {
            dir = item.GetDirection(gameObject, targetProvider?.CurrentTarget, out bool reachedDestination);

            if (reachedDestination || dir != Vector2.zero)
                break;
        }

        MovementIntention = dir;
        return MovementIntention;
    }
}
