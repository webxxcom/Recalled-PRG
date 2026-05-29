using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class AgressionComponent : MonoBehaviour, ITargetProvider
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
        healthComponent.OnDamageTaken += BecomeAgressive;
    }

    public void BecomeAgressive(GameObject @object)
    {
        CurrentTarget = @object;
    }
}
