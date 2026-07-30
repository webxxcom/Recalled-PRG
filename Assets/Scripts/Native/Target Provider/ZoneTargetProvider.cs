using UnityEngine;

public class ZoneTargetProvider : TargetProvider
{
    [Header("Listens to")]
    [SerializeField] GameobjectGameEvent OnTargetEnteredZone;
    [SerializeField] GameobjectGameEvent OnTargetLeftZone;

    void SetTarget(GameObject trgt) => CurrentTarget = trgt;

    void UnsetTarget(GameObject _) => CurrentTarget = null;

    public override TargetProvider Init(TargetProviderSO other)
    {
        ZoneTargetProviderSO zoneTargetProviderSO = other as ZoneTargetProviderSO;

        OnTargetEnteredZone = zoneTargetProviderSO.OnTargetEnteredZone;
        OnTargetLeftZone = zoneTargetProviderSO.OnTargetLeftZone;
        OnTargetEnteredZone.OnEventRaised += SetTarget;
        OnTargetLeftZone.OnEventRaised += UnsetTarget;

        return this;
    }
}
