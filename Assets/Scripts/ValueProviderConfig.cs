using UnityEngine;

[CreateAssetMenu(menuName = "Health/Config")]
public class ValueProviderConfig : ScriptableObject
{
    [field: SerializeField] public int MaximumValue { get; private set; }
    [field: SerializeField] public int CurrentValue { get; private set; }
    [field: SerializeField] public bool IsStatic { get; private set; }
}
