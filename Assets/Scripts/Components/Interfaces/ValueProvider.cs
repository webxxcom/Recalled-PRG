using UnityEngine;

public abstract class ValueProvider : MonoBehaviour
{
    [field: SerializeField] public ValueProviderConfig Config { get; private set; }
    [field: SerializeField] public ValueProviderSO Health { get; protected set; }
}
