using UnityEngine;
using System.Collections;

public class ChaseMovementComponent : MovementStrategy
{
    [SerializeField] float minDistanceToTarget;

    public override Vector2 GetDirection(GameObject target, out bool reachedDestination)
    {
        reachedDestination = false;

        if (target == null)
            return Vector2.zero;

        Vector2 diff = target.transform.position - gameObject.transform.position;
        if (diff.magnitude <= minDistanceToTarget)
        {
            reachedDestination = true;
            return Vector2.zero;
        }

        return diff.normalized;
    }
}
