using UnityEngine;

public class ZoneTargetProvider : TargetProvider
{
    [Header("Listens to")]
    [SerializeField] GameobjectGameEvent OnTargetEnteredZone;
    [SerializeField] GameobjectGameEvent OnTargetLeftZone;

    void SetTarget(GameObject gameObject)
    {
        CurrentTarget = gameObject != null
            ? gameObject.GetComponentInChildren<HealthResource>().gameObject
            : null;
    }

    public override TargetProvider Init(TargetProviderSO other)
    {
        ZoneTargetProviderSO zoneTargetProviderSO = other as ZoneTargetProviderSO;

        OnTargetEnteredZone = zoneTargetProviderSO.OnTargetEnteredZone;
        OnTargetLeftZone = zoneTargetProviderSO.OnTargetLeftZone;
        if (OnTargetEnteredZone) OnTargetEnteredZone.OnEventRaised += SetTarget;
        if (OnTargetLeftZone) OnTargetLeftZone.OnEventRaised += SetTarget;

        return this;
    }
}
