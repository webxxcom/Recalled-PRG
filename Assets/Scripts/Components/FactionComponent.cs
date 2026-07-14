using UnityEngine;

public class FactionComponent : MonoBehaviour
{
    [field: SerializeField] public FactionDefinition Faction { get; private set; }
}
