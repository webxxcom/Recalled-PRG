using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Zone")]
public class ZoneTargetProviderSO : TargetProviderSO
{
    [Header("Listens to")]
    [SerializeField] public GameobjectGameEvent OnTargetEnteredZone;
    [SerializeField] public GameobjectGameEvent OnTargetLeftZone;

    public override TargetProvider CreateInstance()
        => new ZoneTargetProvider().Init(this);
}
