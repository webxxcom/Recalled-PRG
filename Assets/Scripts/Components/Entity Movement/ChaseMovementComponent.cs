using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityMovementComponent))]
public class ChaseMovementComponent : MonoBehaviour, IMovementStrategy
{
    [SerializeField] float minDistanceToTarget;

    public Vector2 GetDirection(GameObject target)
    {
        Vector2 diff = target.transform.position - gameObject.transform.position;
        if (diff.magnitude <= minDistanceToTarget)
            return Vector2.zero;

        return diff.normalized;
    }
}
