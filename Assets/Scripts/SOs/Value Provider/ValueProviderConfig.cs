using UnityEngine;

[CreateAssetMenu(menuName = "Health / Config")]
public class ValueProviderConfig : ScriptableObject
{
    [field: SerializeField] public int MaximumValue { get; private set; }
    [field: SerializeField] public IntVariable CurrentValue { get; private set; }
    [field: SerializeField] public bool IsInfinite { get; private set; }
}
