using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour
{
    public ItemInstance Item { get; private set; }
    public Image Image { get; private set; }
    public TextMeshProUGUI CountText { get; private set; }
    public bool IsRemovable { get; private set; }
    public bool IsEquipped { get; set; }

    private void Awake()
    {
        Image = GetComponent<Image>();

        CountText = Utils.FindOrThrow(GetComponentInChildren<TextMeshProUGUI>);
    }

    public void Initialize(ItemInstance itemInstance, bool isRemovable = true, bool isEquipped = false)
    {
        Item = itemInstance;

        IsRemovable = isRemovable;
        IsEquipped = isEquipped;
        Image.sprite = itemInstance.Definition.Icon;
        CountText.text = itemInstance.Count != 1 ? itemInstance.Count.ToString() : null;
    }
}
