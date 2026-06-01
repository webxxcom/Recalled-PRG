using UnityEngine;

public class ZoneBehaviorComponent: MonoBehaviour, ITargetProvider
{
    [field: SerializeField] int priority;
    [SerializeField] ChaseZoneComponent chaseZoneComponent;

    [field: SerializeField] public GameObject CurrentTarget { get; set; }

    public int Priority => priority;

    private void Awake()
    {
        if (!chaseZoneComponent)
        {
            Debug.LogError("ZoneBehavior component MUST have the ChaseZoneComponent attached");
            return; 
        }

        chaseZoneComponent.OnTargetEnteredTheZone += target => CurrentTarget = target;
        chaseZoneComponent.OnTargetLeftTheZone += () => CurrentTarget = null;
    }
}
