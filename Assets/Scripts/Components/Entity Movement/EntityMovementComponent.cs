using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovementStrategy))]
public class EntityMovementComponent : MovementBase
{
    [field: SerializeField] MovementStrategy[] movementStrategies;
    ITargetProvider[] targetProviders;

    private void Awake()
    {
        targetProviders = GetComponents<ITargetProvider>().OrderBy(o => o.Priority).ToArray();
    }

    protected override Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        ITargetProvider targetProvider = targetProviders
            .FirstOrDefault(e => e.HasTarget && e.CurrentTarget.TryGetComponent(out EntityController ec) && !ec.IsDead);

        Vector2 dir = Vector2.zero;
        foreach (var item in movementStrategies)
        {
            dir = item.GetDirection(targetProvider?.CurrentTarget, out bool reachedDestination);

            if (reachedDestination || dir != Vector2.zero)
                break;
        }

        MovementIntention = dir;
        return MovementIntention * WalkingSpeed;
    }
}
