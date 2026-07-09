using UnityEngine;

[CreateAssetMenu(fileName = "GeneralInventoryItem", menuName = "Inventory Items/General")]
public class ItemDefinition : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public bool IsStockable { get; private set; }

    public virtual ItemInstance CreateInstance() => new(this);
}
