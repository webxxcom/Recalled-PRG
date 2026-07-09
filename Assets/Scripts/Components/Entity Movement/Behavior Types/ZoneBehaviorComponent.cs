using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoneBehaviorComponent : TargetProvider
{
    [SerializeField] ChaseZone chaseZoneComponent;

    private void OnEnable ()
    {
        chaseZoneComponent.OnTargetEnteredTheZone += SetTarget;
        chaseZoneComponent.OnTargetLeftTheZone += UnsetTarget;
    }

    private void OnDisable()
    {
        chaseZoneComponent.OnTargetEnteredTheZone -= SetTarget;
        chaseZoneComponent.OnTargetLeftTheZone -= UnsetTarget;
    }

    void SetTarget(GameObject trgt) => CurrentTarget = trgt;

    void UnsetTarget(GameObject _) => CurrentTarget = null;
}
