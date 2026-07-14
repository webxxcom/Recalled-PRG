using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _basicItemsInventoryGrid;
    [SerializeField] GameObject _inventoryItemPrefab;
    [SerializeField] InventorySlot _swordInventoryItem;
    [SerializeField] InventorySlot _armorInventoryItem;
    [SerializeField] InventorySlot _bootsInventoryItem;
    [SerializeField] Sprite _absentSwordSprite;
    [SerializeField] Sprite _absentArmorSprite;
    [SerializeField] Sprite _absentBootsSprite;
    [SerializeField] GameObject _highlighter;
    [SerializeField] InputActionAsset _inputActionAsset;

    UIEventRaiser _uiEventRaiser;
    Canvas _canvas;
    PlayerInput _playerInput;
    PlayerInventory _playerInventory;
    DescriptionManager _descriptionManager;
    InventorySlot _selectedInventorySlot;
    readonly List<GameObject> _createdInventoryItems = new();

    public bool IsActive => _canvas.enabled;

    private void Awake()
    {
        _canvas = Utils.FindOrThrow(GetComponentInChildren<Canvas>);

        _uiEventRaiser = Utils.FindOrThrow(FindAnyObjectByType<UIEventRaiser>);
        _descriptionManager = Utils.FindOrThrow(FindAnyObjectByType<DescriptionManager>);
        _playerInput = Utils.FindOrThrow(FindAnyObjectByType<PlayerInput>);
        _playerInventory = Utils.FindOrThrow(FindAnyObjectByType<PlayerInventory>);
    }

    private void Start()
    {
        _canvas.enabled = false;
    }

    void ToggleInventory(InputAction.CallbackContext cc)
    {
        if (IsActive) Close();
        else Open();
    }

    private void OnEnable()
    {
        _inputActionAsset.FindActionMap("Player").FindAction("InventoryToggle").performed += ToggleInventory;
        _inputActionAsset.FindActionMap("Inventory").FindAction("InventoryToggle").performed += ToggleInventory;

        _uiEventRaiser.OnUIElementSelected += ItemSelected;
        _uiEventRaiser.OnUIElementDeselected += ItemDeselected;
    }

    private void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").FindAction("InventoryToggle").performed -= ToggleInventory;
        _inputActionAsset.FindActionMap("Inventory").FindAction("InventoryToggle").performed -= ToggleInventory;

        _uiEventRaiser.OnUIElementSelected -= ItemSelected;
        _uiEventRaiser.OnUIElementDeselected -= ItemDeselected;
    }

    void CreateGeneralItemSlot(ItemInstance itemInstance)
    {
        GameObject inventoryItem = Instantiate(_inventoryItemPrefab, _basicItemsInventoryGrid.transform);

        inventoryItem.GetComponent<InventorySlot>().Initialize(itemInstance);
        _createdInventoryItems.Add(inventoryItem);
    }
    void RefreshGeneralSlots() =>  _playerInventory.Items.ForEach(CreateGeneralItemSlot);

    void RefreshEquipSlots()
    {
        if (_playerInventory.Sword != null)
            _swordInventoryItem.Initialize(_playerInventory.Sword, false, true);
        else _swordInventoryItem.Absent(_absentSwordSprite);

        if (_playerInventory.Armor != null)
            _armorInventoryItem.Initialize(_playerInventory.Armor, false, true);
        else _armorInventoryItem.Absent(_absentArmorSprite);

        if (_playerInventory.Boots != null)
            _bootsInventoryItem.Initialize(_playerInventory.Boots, false, true);
        else _bootsInventoryItem.Absent(_absentBootsSprite);
    }

    public void Open()
    {
        _canvas.enabled = true;
        _playerInput.SwitchCurrentActionMap("Inventory");

        RefreshGeneralSlots();
        RefreshEquipSlots();
    }

    public void Close()
    {
        _createdInventoryItems.ForEach(ii => Destroy(ii));
        _createdInventoryItems.Clear();

        _canvas.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
        _highlighter.SetActive(false);
    }

    public void ItemSelected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out InventorySlot inventorySlot))
        {
            _selectedInventorySlot = inventorySlot;

            _highlighter.SetActive(true);
            _highlighter.transform.position = gameObject.transform.position;
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
            ItemInstance unequipped = equippable.Unequip(_playerInventory);

            CreateGeneralItemSlot(unequipped);
            ItemDeselected();
            RefreshEquipSlots();
        }
    }

    void RemoveItem(InventorySlot inventorySlot)
    {
        _playerInventory.Remove(inventorySlot.Item);
        Destroy(inventorySlot.gameObject);
        ItemDeselected();
    }

    void EquipItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.Item is IEquippable equippable)
        {
            ItemInstance replaced = equippable.Equip(_playerInventory);

            if (replaced != null)
                inventorySlot.Initialize(replaced);
            else
                RemoveItem(inventorySlot);

            RefreshEquipSlots();
        }
    }
}
