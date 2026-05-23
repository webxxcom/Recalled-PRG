using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assemblies;

public class ChaseComponent : MonoBehaviour
{
    [SerializeField] float minDistanceToTarget;

    public GameObject Target { get; set; }

    public Vector2 GetDirection()
    {
        if (Target == null)
            return Vector2.zero;

        Vector2 diff = Target.transform.position - gameObject.transform.position;
        if (diff.magnitude <= minDistanceToTarget)
            return Vector2.zero;

        return diff.normalized;
    }
}
