using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Items/Boots")]
public class BootsInventoryItem : InventoryItem
{
    [field: SerializeField] public float SpeedMultiplier { get; private set; }
}
