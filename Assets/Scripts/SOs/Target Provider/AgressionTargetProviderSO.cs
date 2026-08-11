using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Aggression")]
public class AgressionTargetProviderSO : TargetProviderSO
{
    public override TargetProvider CreateInstance()
        => new AggressionTargetProvider().Init(this);
}
