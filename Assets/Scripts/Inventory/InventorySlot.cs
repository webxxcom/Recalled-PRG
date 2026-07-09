using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour
{
    public ItemInstance Item { get; private set; }
    public Image Image { get; private set; }
    public TextMeshProUGUI CountText { get; private set; }

    private void Awake()
    {
        Image = GetComponent<Image>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(ItemDefinition itemDefinition)
    {
        Initialize(itemDefinition.CreateInstance());
    }

    public void Initialize(ItemInstance itemInstance)
    {
        Item = itemInstance;

        Image.sprite = Item.Definition.Icon;
        CountText.text = Item.Count != 1 ? Item.Count.ToString() : null;
    }
}
