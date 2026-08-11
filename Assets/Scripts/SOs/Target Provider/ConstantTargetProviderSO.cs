using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Constant")]
public class ConstantTargetProviderSO : TargetProviderSO
{
    [field: SerializeField] public GameObject Target { get; private set; }
   
    public override TargetProvider CreateInstance()
        => new ConstantTargetProvider().Init(this);
}
