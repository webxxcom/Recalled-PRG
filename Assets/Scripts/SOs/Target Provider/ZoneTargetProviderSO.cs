using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Zone")]
public class ZoneTargetProviderSO : TargetProviderSO
{
    [Header("Listens to")]
    public VoidGameEvent OnTargetEnteredZone;
    public VoidGameEvent OnTargetLeftZone;

    public override TargetProvider CreateInstance()
        => new ZoneTargetProvider().Init(this);
}
