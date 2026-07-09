using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemScript : MonoBehaviour
{
    public InventoryItem InventoryItem { get; private set; }
    public Image Image { get; private set; }
    public TextMeshProUGUI CountText { get; private set; }

    private void Awake()
    {
        Image = GetComponent<Image>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(InventoryItem inventoryItem, int count = 1)
    {
        InventoryItem = inventoryItem;

        Image.sprite = inventoryItem.Icon;
        CountText.text = count != 1 ? count.ToString() : null;
    }
}
