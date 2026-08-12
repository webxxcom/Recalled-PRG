using UnityEngine;

public class ZoneTargetProvider : TargetProvider
{
    [Header("Listens to")]
    [SerializeField] VoidGameEvent OnTargetEnteredZone;
    [SerializeField] VoidGameEvent OnTargetLeftZone;

    void SetTarget() => CurrentTarget = GameObject.FindAnyObjectByType<PlayerController>().gameObject;

    void UnsetTarget() => CurrentTarget = null;

    public override TargetProvider Init(TargetProviderSO other)
    {
        ZoneTargetProviderSO zoneTargetProviderSO = other as ZoneTargetProviderSO;

        OnTargetEnteredZone = zoneTargetProviderSO.OnTargetEnteredZone;
        OnTargetLeftZone = zoneTargetProviderSO.OnTargetLeftZone;
        if (OnTargetEnteredZone) OnTargetEnteredZone.OnEventRaised += SetTarget;
        if (OnTargetLeftZone) OnTargetLeftZone.OnEventRaised += UnsetTarget;

        return this;
    }
}
