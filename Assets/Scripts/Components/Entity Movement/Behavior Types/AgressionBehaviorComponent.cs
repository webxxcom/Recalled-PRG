using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class AgressionBehaviorComponent: MonoBehaviour, ITargetProvider
{
    [SerializeField] int priority;
    [field: SerializeField] public GameObject CurrentTarget { get; set; }

    public int Priority => priority;

    HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        healthComponent.OnValueChanged += (obj, val) => BecomeAgressive(obj);
    }

    public void BecomeAgressive(GameObject gameObject)
    {
        CurrentTarget = gameObject;
    }
}
