using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Items/Boots")]
public class BootsDefinition : ItemDefinition
{
    [field: SerializeField] public float SpeedMultiplier { get; private set; }
}
