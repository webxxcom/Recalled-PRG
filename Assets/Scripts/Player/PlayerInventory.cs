using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] SwordDefinition _basicSwordPrefab;
    [SerializeField] ArmorDefinition _basicArmorPrefab;
    [SerializeField] BootsDefinition _basicBootsPrefab;

    public SwordDefinition Sword { get; set; }
    public ArmorDefinition Armor { get; set; }
    public BootsDefinition Boots { get; set; }
    public List<ItemInstance> Items { get; } = new();

    CoinCountScript _coinCountScript;

    private void Awake()
    {
        _coinCountScript = GetComponentInChildren<CoinCountScript>();
    }

    private void Start()
    {
        Sword = _basicSwordPrefab;
        Armor = _basicArmorPrefab;
        Boots = _basicBootsPrefab;
    }

    public void Add(ItemDefinition item, int count = 1)
    {
        if (count <= 0)
            return;

        // If we can stack item then try to find it
        if (item.IsStockable)
        {
            foreach (var ii in Items)
            {
                // The item definitions match then add count
                if (ii.Definition == item)
                {
                    ii.Count += count;
                    return;
                }
            }
        }

        // Even if item already exists it's not stackable so add it
        Items.Add(item.CreateInstance());
    }

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
}
