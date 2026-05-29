using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assemblies;

public class ChaseComponent : MonoBehaviour, ITargetProvider
{
    [SerializeField] int priority;
    [SerializeField] ChaseZoneComponent chaseZoneComponent;

    [field: SerializeField] public GameObject CurrentTarget { get; set; }

    public int Priority => priority;

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
}
