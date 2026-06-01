using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IMovementStrategy))]
public class EntityMovementComponent : MovementBase
{
    ITargetProvider[] targetProviders;
    IMovementStrategy movementStrategy;

    private void Awake()
    {
        movementStrategy = GetComponent<IMovementStrategy>();
        targetProviders = GetComponents<ITargetProvider>().OrderBy(o => o.Priority).ToArray();
    }

    Vector2 GetMovementIntention()
    {
        if (MovementIntention != Vector2.zero)
            LastMovement = MovementIntention;

        foreach (var item in targetProviders)
        {
            if (item.HasTarget)
            {
                if (item.CurrentTarget.TryGetComponent(out EntityController ec) && ec.IsDead)
                    continue;

                return MovementIntention = movementStrategy.GetDirection(item.CurrentTarget);
            }
        }
        return Vector2.zero;
    }

    public override Vector2 GetFinalMovement()
    {
        Vector2 finalMovement = GetMovementIntention();

        return finalMovement * WalkingSpeed;
    }
}
