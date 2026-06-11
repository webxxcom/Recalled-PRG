using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryComponent : MonoBehaviour
{
    public int Coins { get; set; }

    readonly Dictionary<InventoryItem, int> items = new();

    CoinCountScript coinCountScript;

    private void Awake()
    {
        coinCountScript = GetComponentInChildren<CoinCountScript>();
    }

    public void Add(InventoryItem item, int count = 1)
    {
        if (item.Name == "Coin")
        {
            Coins += count;

            coinCountScript.ChangeValue(Coins);
        }
        else if (!items.TryAdd(item, count))
        {
            items[item] += count;
        }
    }
}
