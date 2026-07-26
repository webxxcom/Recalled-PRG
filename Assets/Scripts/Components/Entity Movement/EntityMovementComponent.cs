using System.Linq;
using UnityEngine;

public class EntityMovementComponent : MovementBase
{
    [SerializeField] MovementStrategy[] _movementStrategies;
    [SerializeField] TargetProvider[] _targetProviders;

    protected override Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        TargetProvider targetProvider = _targetProviders
            .FirstOrDefault(e => e.HasTarget);
            
        Vector2 dir = Vector2.zero;
        foreach (var item in _movementStrategies)
        {
            dir = item.GetDirection(gameObject, targetProvider != null ? targetProvider.CurrentTarget : null, out bool reachedDestination);

            if (reachedDestination || dir != Vector2.zero)
                break;
        }

        MovementIntention = dir;
        return MovementIntention * WalkingSpeed;
    }
}
