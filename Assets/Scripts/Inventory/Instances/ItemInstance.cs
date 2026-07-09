using UnityEngine;

public class ItemInstance
{
    public ItemDefinition Definition { get; private set; }

    public int Count { get; set; } = 1;

    public virtual string Description => Definition.Description;

    public virtual ItemInstance CreateInstance(ItemDefinition itemDefinition) => new(itemDefinition);

    public ItemInstance(ItemDefinition itemDefinition)
    {
        Definition = itemDefinition;
    }
}