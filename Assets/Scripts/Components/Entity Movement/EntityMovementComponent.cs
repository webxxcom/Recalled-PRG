using System;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

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

        return MovementIntention = movementStrategy.GetDirection();
    }

    public override Vector2 GetFinalMovement()
    {
        Vector2 finalMovement = GetMovementIntention();

        return finalMovement * WalkingSpeed;
    }
}
