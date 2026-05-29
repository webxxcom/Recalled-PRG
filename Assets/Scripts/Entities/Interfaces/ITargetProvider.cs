using UnityEngine;

public interface ITargetProvider
{
    int Priority { get; }
    public bool HasTarget => CurrentTarget != null;

    public GameObject CurrentTarget { get; set; }
}
