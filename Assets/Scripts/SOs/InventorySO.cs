using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player Inventory")]
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


#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < Items.Count; i++)
            if (Items[i].Definition != null)
                Items[i] = Items[i].Definition.CreateInstance();
    }
#endif
}
