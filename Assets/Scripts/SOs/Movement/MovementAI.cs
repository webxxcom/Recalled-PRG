using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement / MovementAI")]
public class MovementAI : ScriptableObject
{
    [SerializeField] MovementStrategy[] _movementStrategies;
    [SerializeField] TargetProvider[] _targetProviders;

    public Vector2 GetMovementIntention(MovementBase movementBase)
    {
        if (movementBase.MovementIntention != Vector2.zero)
            movementBase.LastMovement = movementBase.MovementIntention;

        TargetProvider targetProvider = _targetProviders
            .FirstOrDefault(e => e.HasTarget);

        Vector2 dir = Vector2.zero;
        foreach (var item in _movementStrategies)
        {
            dir = item.GetDirection(movementBase.gameObject, targetProvider != null ? targetProvider.CurrentTarget : null, out bool reachedDestination);

            if (reachedDestination || dir != Vector2.zero)
                break;
        }

        movementBase.MovementIntention = dir;
        return movementBase.MovementIntention;
    }
}
