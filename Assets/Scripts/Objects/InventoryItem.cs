using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory item")]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
