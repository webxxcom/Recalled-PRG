using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [field: SerializeField] public ItemDefinition Definition { get; private set; }

    [field: SerializeField] public int Count { get; set; } = 1;

    public virtual string Description => Definition.Description;
    public virtual bool IsEquippable => false;

    public virtual ItemInstance CreateInstance(ItemDefinition itemDefinition) => new(itemDefinition);

    public ItemInstance(ItemDefinition itemDefinition)
    {
        Definition = itemDefinition;
    }

    public virtual void Equip(PlayerInventory _)
    {
    }
}