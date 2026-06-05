using System.Linq;
using UnityEditor.ShaderGraph.Configuration;
using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(IMovementStrategy))]
public class EntityMovementComponent : MovementBase
{
    ITargetProvider[] targetProviders;
    IMovementStrategy[] movementStrategy;

    private void Awake()
    {
        movementStrategy = GetComponents<IMovementStrategy>();
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
                
                MovementIntention = movementStrategy[0].GetDirection(item.CurrentTarget);
                return MovementIntention;
            }
        }

        MovementIntention = movementStrategy[1].GetDirection(null);
        return MovementIntention;
    }

    public override Vector2 GetFinalMovement()
    {
        Vector2 finalMovement = GetMovementIntention();

        return finalMovement * WalkingSpeed;
    }
}
