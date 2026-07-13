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

    PlayerInventory _playerInventory;

    private void Awake()
    {
        Image = GetComponent<Image>();

        _playerInventory = Utils.FindOrThrow(FindAnyObjectByType<PlayerInventory>);
        CountText = Utils.FindOrThrow(GetComponentInChildren<TextMeshProUGUI>);
    }

    public void Initialize(ItemDefinition itemDefinition)
    {
        Initialize(itemDefinition.CreateInstance());
    }
    public void Initialize(ItemInstance itemInstance) => Initialize(itemInstance, false);

    public void Initialize(ItemInstance itemInstance, bool isRemovable = false)
    {
        Item = itemInstance;

        IsRemovable = isRemovable;
        Image.sprite = itemInstance.Definition.Icon;
        CountText.text = itemInstance.Count != 1 ? itemInstance.Count.ToString() : null;
    }
}
