using UnityEngine;

public class ChaseComponent : MonoBehaviour
{
    [SerializeField] float minDistanceToTarget;

    public Transform Target { get; set; }

    public Vector2 GetDirection()
    {
        if (Target == null)
            return Vector2.zero;

        Vector2 diff = Target.position - gameObject.transform.position;
        if (diff.magnitude <= minDistanceToTarget)
            return Vector2.zero;

        return diff.normalized;
    }
}
