using UnityEngine;

[RequireComponent(typeof(HealthProvider))]
public class AgressionBehaviorComponent : TargetProvider
{
    HealthProvider healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthProvider>();
    }

    private void OnEnable()
    {
        healthComponent.OnValueChanged += BecomeAgressive;
    }

    private void OnDisable()
    {
        healthComponent.OnValueChanged -= BecomeAgressive;
    }

    public void BecomeAgressive(GameObject gameObject, int _)
    {
        CurrentTarget = gameObject;
    }
}
