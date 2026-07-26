using UnityEngine;

[CreateAssetMenu(menuName = "Behavior / Zone")]
public class ZoneBehaviorSO : TargetProvider
{
    [Header("Listens to")]
    [SerializeField] GameobjectGameEvent OnTargetEnteredZone;
    [SerializeField] GameobjectGameEvent OnTargetLeftZone;

    private void OnEnable ()
    {
        OnTargetEnteredZone.OnEventRaised += SetTarget;
        OnTargetLeftZone.OnEventRaised += UnsetTarget;
    }

    private void OnDisable()
    {
        OnTargetEnteredZone.OnEventRaised -= SetTarget;
        OnTargetLeftZone.OnEventRaised -= UnsetTarget;
    }

    void SetTarget(GameObject trgt) => CurrentTarget = trgt;

    void UnsetTarget(GameObject _) => CurrentTarget = null;
}
