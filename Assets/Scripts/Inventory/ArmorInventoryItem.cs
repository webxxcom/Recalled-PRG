using UnityEngine;

[CreateAssetMenu(fileName = "ArmorInventoryItem", menuName = "Inventory Items/Armor")]
public class ArmorInventoryItem : InventoryItem
{
    [field: SerializeField] public float Protection { get; private set; }
    [field: SerializeField] public float Weight { get; private set; }
}
