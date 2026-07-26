using UnityEngine;

[CreateAssetMenu(menuName = "Movements/Chasing")]
public class ChaseMovementSO : MovementStrategy
{
    [SerializeField] float _minDistanceToTarget;

    public override Vector2 GetDirection(GameObject origin, GameObject target, out bool reachedDestination)
    {
        reachedDestination = false;

        if (target == null)
            return Vector2.zero;

        Vector2 diff = target.transform.position - origin.transform.position;
        if (diff.magnitude <= _minDistanceToTarget)
        {
            reachedDestination = true;
            return Vector2.zero;
        }

        return diff.normalized;
    }
}
