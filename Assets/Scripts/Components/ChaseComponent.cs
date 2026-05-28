using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assemblies;

public class ChaseComponent : MonoBehaviour
{
    [SerializeField] float minDistanceToTarget;
    [SerializeField] ChaseZoneComponent chaseZoneComponent;
    [field: SerializeField] public GameObject CurrentTarget { get; set; }

    void SetCurrentTarget(GameObject target)
    {
        CurrentTarget = target;
    }

    void RemoveCurrentTarget()
    {
        CurrentTarget = null;
    }

    private void Awake()
    {
        chaseZoneComponent.OnTargetEnteredTheZone += SetCurrentTarget;
        chaseZoneComponent.OnTargetLeftTheZone += RemoveCurrentTarget;
    }

    public Vector2 GetDirection()
    {
        if (CurrentTarget == null || CurrentTarget.GetComponent<EntityController>().IsDead)
            return Vector2.zero;

        Vector2 diff = CurrentTarget.transform.position - gameObject.transform.position;
        if (diff.magnitude <= minDistanceToTarget)
            return Vector2.zero;

        return diff.normalized;
    }
}
