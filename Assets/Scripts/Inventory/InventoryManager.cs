using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySO _inventory;

    [Header("UI")]
    [SerializeField] GameObject _basicItemsInventoryGrid;
    [SerializeField] GameObject _inventoryItemPrefab;
    [SerializeField] InventorySlot _swordInventoryItem;
    [SerializeField] InventorySlot _armorInventoryItem;
    [SerializeField] InventorySlot _bootsInventoryItem;
    [SerializeField] Sprite _absentSwordSprite;
    [SerializeField] Sprite _absentArmorSprite;
    [SerializeField] Sprite _absentBootsSprite;
    [SerializeField] GameObject _highlighter;

    [Header("Listens to")]
    [SerializeField] VoidGameEvent OnInventory;

    UIEventRaiser _uiEventRaiser;
    Canvas _canvas;
    DescriptionManager _descriptionManager;
    InventorySlot _selectedInventorySlot;
    readonly List<GameObject> _createdInventorySlots = new();

    public bool IsActive
    {
        get => _canvas.enabled;
        set
        {
            if (value) Open();
            else Close();
        }
    }

    private void Awake()
    {
        _canvas = Utils.FindOrThrow(GetComponentInChildren<Canvas>);

        _uiEventRaiser = Utils.FindOrThrow(FindAnyObjectByType<UIEventRaiser>);
        _descriptionManager = Utils.FindOrThrow(FindAnyObjectByType<DescriptionManager>);
    }

    void ToggleInventory() => IsActive = !IsActive;

    private void OnEnable()
    {
        OnInventory.OnEventRaised += ToggleInventory;

        _uiEventRaiser.OnUIElementSelected += ItemSelected;
        _uiEventRaiser.OnUIElementDeselected += ItemDeselected;
    }

    private void OnDisable()
    {
        OnInventory.OnEventRaised -= ToggleInventory;

        _uiEventRaiser.OnUIElementSelected -= ItemSelected;
        _uiEventRaiser.OnUIElementDeselected -= ItemDeselected;
    }

    void CreateGeneralItemSlot(ItemInstance itemInstance)
    {
        GameObject inventoryItem = Instantiate(_inventoryItemPrefab, _basicItemsInventoryGrid.transform);

        inventoryItem.GetComponent<InventorySlot>().Initialize(itemInstance);
        _createdInventorySlots.Add(inventoryItem);
    }
    void RefreshGeneralSlots() => _inventory.Items.ForEach(CreateGeneralItemSlot);

    void RefreshEquipSlots()
    {
        if (_inventory.Sword != null) _swordInventoryItem.Initialize(_inventory.Sword, false, true);
        else _swordInventoryItem.Absent(_absentSwordSprite);

        if (_inventory.Armor != null) _armorInventoryItem.Initialize(_inventory.Armor, false, true);
        else _armorInventoryItem.Absent(_absentArmorSprite);

        if (_inventory.Boots != null) _bootsInventoryItem.Initialize(_inventory.Boots, false, true);
        else _bootsInventoryItem.Absent(_absentBootsSprite);
    }

    public void Open()
    {
        RefreshGeneralSlots();
        RefreshEquipSlots();
    }

    public void Close()
    {
        _createdInventorySlots.ForEach(ii => Destroy(ii));
        _createdInventorySlots.Clear();

        _highlighter.SetActive(false);
    }

    public void ItemSelected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out InventorySlot inventorySlot))
        {
            _selectedInventorySlot = inventorySlot;

            if (inventorySlot.Item == null)
                return;

            if (gameObject.TryGetComponent(out InventorySlot _))
            {
                _highlighter.SetActive(true);
                _highlighter.transform.position = gameObject.transform.position;
            }
            _descriptionManager.Show(inventorySlot);
        }
    }

    public void ItemDeselected()
    {
        _selectedInventorySlot = null;

        _highlighter.SetActive(false);
        _descriptionManager.Hide();
    }

    public void OnRemoveButtonClick() => RemoveItem(_selectedInventorySlot);
    public void OnEquipButtonClick() => EquipItem(_selectedInventorySlot);
    public void OnUnequipButtonClick() => UnequipItem(_selectedInventorySlot);

    void UnequipItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.Item is IEquippable equippable)
        {
            ItemInstance unequipped = equippable.Unequip(_inventory);

            CreateGeneralItemSlot(unequipped);
            ItemDeselected();
            RefreshEquipSlots();
        }
    }

    void RemoveItem(InventorySlot inventorySlot)
    {
        _inventory.Remove(inventorySlot.Item);
        Destroy(inventorySlot.gameObject);
        ItemDeselected();
    }

    void EquipItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.Item is IEquippable equippable)
        {
            ItemInstance replaced = equippable.Equip(_inventory);

            if (replaced != null)
                inventorySlot.Initialize(replaced);
            else
                RemoveItem(inventorySlot);

            RefreshEquipSlots();
        }
    }
}
