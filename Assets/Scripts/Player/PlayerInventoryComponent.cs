using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryComponent : MonoBehaviour
{
    [SerializeField] SwordInventoryItem _basicSwordPrefab;
    [SerializeField] ArmorInventoryItem _basicArmorPrefab;

    public SwordInventoryItem Sword { get; set; }
    public ArmorInventoryItem Armor { get; set; }
    public int Coins { get; set; }
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
    }

    public void Add(InventoryItem item, int count = 1)
    {
        if (item.Name == "Coin")
        {
            Coins += count;

            _coinCountScript.ChangeValue(Coins);
        }
        else if (!Items.TryAdd(item, count))
        {
            Items[item] += count;
        }
    }

    public bool Contains(InventoryItem item) => Items.ContainsKey(item);
}
