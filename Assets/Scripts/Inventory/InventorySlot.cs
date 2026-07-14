using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class InventorySlot : MonoBehaviour
{
    public ItemInstance Item { get; private set; }
    public Image Image { get; private set; }
    public TextMeshProUGUI CountText { get; private set; }
    public bool IsRemovable { get; private set; }
    public bool IsEquippable => Item is IEquippable;
    public bool IsEquipped { get; set; }

    Button _button;

    private void Awake()
    {
        Image = GetComponent<Image>();
        _button = GetComponent<Button>();

        CountText = Utils.FindOrThrow(GetComponentInChildren<TextMeshProUGUI>);
    }

    public void Absent(Sprite sprite)
    {
        _button.enabled = false;
        IsRemovable = false;
        IsEquipped = true;
        Image.sprite = sprite;
        CountText.text = null;
    }

    public void Initialize(ItemInstance itemInstance, bool isRemovable = true, bool isEquipped = false)
    {
        Item = itemInstance;

        _button.enabled = true;
        IsRemovable = isRemovable;
        IsEquipped = isEquipped;
        Image.sprite = itemInstance.Definition.Icon;
        CountText.text = itemInstance.Count != 1 ? itemInstance.Count.ToString() : null;
    }
}
