using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "InventorySO")]
public class InventorySO : ScriptableObject
{
    [field: SerializeField] public List<ItemInstance> Items { get; private set; }

    public Sword Sword { get; set; }
    public Armor Armor { get; set; }
    public Boots Boots { get; set; }

    public void Add(ItemInstance itemInstance, int count = 1)
    {
        if (count <= 0 || itemInstance == null)
            return;

        // If we can stack item then try to find it
        if (itemInstance.Definition.IsStockable)
        {
            foreach (var ii in Items)
            {
                // The item definitions match then add count
                if (ii.Definition == itemInstance.Definition)
                {
                    ii.Count += count;
                    return;
                }
            }
        }

        // Even if item already exists it's not stackable so add it
        Items.Add(itemInstance);
    }

    public void Add(ItemDefinition item, int count = 1) => Add(item.CreateInstance(), count);

    public bool Contains(ItemDefinition item) => Items.Any(ii => ii.Definition == item);

    public void RemoveAll(ItemDefinition item)
    {
        foreach (var ii in Items)
        {
            if (ii.Definition == item)
            {
                Items.Remove(ii);
                return;
            }
        }
    }

    public void Remove(ItemDefinition item, int count = 1)
    {
        if (count <= 0)
            return;

        foreach (var ii in Items)
        {
            if (ii.Definition == item)
            {
                if (ii.Count - count <= 0)
                    Items.Remove(ii);
                else
                    ii.Count -= count;

                return;
            }
        }
    }

    public void Remove(ItemInstance itemInstance) => Items.Remove(itemInstance);
}
