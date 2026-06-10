using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(MovementStrategy))]
public class EntityMovementComponent : MovementBase
{
    [field: SerializeField] MovementStrategy[] movementStrategies;
    ITargetProvider[] targetProviders;

    private void Awake()
    {
        targetProviders = GetComponents<ITargetProvider>().OrderBy(o => o.Priority).ToArray();
    }

    Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        ITargetProvider targetProvider = targetProviders
            .FirstOrDefault(e => e.HasTarget && e.CurrentTarget.TryGetComponent(out EntityController ec) && !ec.IsDead);

        Vector2 dir = Vector2.zero;
        foreach (var item in movementStrategies)
        {
            dir = item.GetDirection(targetProvider?.CurrentTarget);

            if (dir != Vector2.zero)
                break;
        }

        MovementIntention = dir;
        return MovementIntention;
    }

    public override Vector2 GetFinalMovement()
    {
        if (!enabled)
            return Vector2.zero;

        Vector2 finalMovement = GetMovementIntention();
        return finalMovement * WalkingSpeed;
    }
}
