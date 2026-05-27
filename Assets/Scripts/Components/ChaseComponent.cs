using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assemblies;

public class ChaseComponent : MonoBehaviour
{
    [SerializeField] float minDistanceToTarget;
    [SerializeField] GameObject currentTarget;

    public GameObject Target { get => currentTarget; set => currentTarget = value; }

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
