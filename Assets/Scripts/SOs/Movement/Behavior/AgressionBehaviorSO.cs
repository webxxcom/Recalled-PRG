using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Aggression")]
public class AgressionBehaviorSO : TargetProvider
{
    private void OnEnable()
    {
        //TODO
        //healthComponent.OnValueChanged += BecomeAgressive;
    }

    private void OnDisable()
    {
        //healthComponent.OnValueChanged -= BecomeAgressive;
    }

    public void BecomeAgressive(GameObject gameObject, int _)
    {
        CurrentTarget = gameObject;
    }
}
