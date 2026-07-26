using UnityEngine;

public abstract class TargetProvider : ScriptableObject
{
    public bool HasTarget => CurrentTarget != null;

    [field: SerializeField] public GameObject CurrentTarget { get; protected set; }
}
