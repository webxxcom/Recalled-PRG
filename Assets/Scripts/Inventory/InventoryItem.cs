using UnityEngine;

[CreateAssetMenu(fileName = "GeneralInventoryItem", menuName = "Inventory Items/General")]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}
