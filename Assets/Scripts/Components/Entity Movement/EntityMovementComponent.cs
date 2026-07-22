using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovementStrategy))]
public class EntityMovementComponent : MovementBase
{
    [field: SerializeField] MovementStrategy[] movementStrategies;
    [field: SerializeField] TargetProvider[] targetProviders;

    protected override Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        TargetProvider targetProvider = targetProviders
            .FirstOrDefault(e => e.HasTarget);
            
        Vector2 dir = Vector2.zero;
        foreach (var item in movementStrategies)
        {
            if (!item.enabled)
                continue;

            dir = item.GetDirection(targetProvider != null ? targetProvider.CurrentTarget : null, out bool reachedDestination);

            if (reachedDestination || dir != Vector2.zero)
                break;
        }

        MovementIntention = dir;
        return MovementIntention * WalkingSpeed;
    }
}
