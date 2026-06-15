using UnityEngine;

public abstract class TargetProvider : MonoBehaviour
{
    public bool HasTarget => CurrentTarget != null;

    [field: SerializeField] public GameObject CurrentTarget { get; protected set; }
}
