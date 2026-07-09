using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryComponent : MonoBehaviour
{
    [SerializeField] SwordInventoryItem _basicSwordPrefab;
    [SerializeField] ArmorInventoryItem _basicArmorPrefab;
    [SerializeField] BootsInventoryItem _basicBootsPrefab;

    public SwordInventoryItem Sword { get; set; }
    public ArmorInventoryItem Armor { get; set; }
    public BootsInventoryItem Boots { get; set; }
    public Dictionary<InventoryItem, int> Items { get; } = new();

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

    public void Add(InventoryItem item, int count = 1)
    {
        if (!Items.TryAdd(item, count))
        {
            Items[item] += count;
        }
    }

    public bool Contains(InventoryItem item) => Items.ContainsKey(item);

    public void Remove(InventoryItem item, int count = 1)
    {
        if (count <= 0)
            return;

        if (Items.ContainsKey(item))
        {
            if (Items[item] - count <= 0)
                Items.Remove(item);
            else
                Items[item] -= count;
        }
    }
}
