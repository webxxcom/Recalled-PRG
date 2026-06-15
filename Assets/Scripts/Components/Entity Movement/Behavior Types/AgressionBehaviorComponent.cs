using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class AgressionBehaviorComponent : TargetProvider
{
    HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
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
