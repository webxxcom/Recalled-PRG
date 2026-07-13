using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [field: SerializeField] GameObject _basicItemsInventoryGrid;
    [field: SerializeField] GameObject _inventoryItemPrefab;
    [field: SerializeField] InventorySlot _swordInventoryItem;
    [field: SerializeField] InventorySlot _armorInventoryItem;
    [field: SerializeField] InventorySlot _bootsInventoryItem;
    [field: SerializeField] GameObject _highlighter;
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

        _uiEventRaiser.OnUIElementSelected += OnSelect;
        _uiEventRaiser.OnUIElementDeselected += OnDeselect;

        _playerInventory.OnSwordEquipped += _swordInventoryItem.Initialize;
    }

    private void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").FindAction("InventoryToggle").performed -= ToggleInventory;
        _inputActionAsset.FindActionMap("Inventory").FindAction("InventoryToggle").performed -= ToggleInventory;

        _uiEventRaiser.OnUIElementSelected -= OnSelect;
        _uiEventRaiser.OnUIElementDeselected -= OnDeselect;

        _playerInventory.OnSwordEquipped -= _swordInventoryItem.Initialize;
    }

    void InitGeneralItems()
    {
        foreach (var item in _playerInventory.Items)
        {
            GameObject inventoryItem = Instantiate(_inventoryItemPrefab, Vector3.zero, Quaternion.identity, _basicItemsInventoryGrid.transform);

            inventoryItem.GetComponent<InventorySlot>().Initialize(item, true);

            _createdInventoryItems.Add(inventoryItem);
        }
    }

    void InitBasicItems()
    {
        _swordInventoryItem.Initialize(_playerInventory.Sword);
        _armorInventoryItem.Initialize(_playerInventory.Armor);
        _bootsInventoryItem.Initialize(_playerInventory.Boots);
    }

    public void Open()
    {
        _canvas.enabled = true;
        _playerInput.SwitchCurrentActionMap("Inventory");

        InitGeneralItems();
        InitBasicItems();
    }

    public void Close()
    {
        _createdInventoryItems.ForEach(ii => Destroy(ii));
        _createdInventoryItems.Clear();

        _canvas.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
        _highlighter.SetActive(false);
    }

    public void OnSelect(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out InventorySlot inventorySlot))
        {
            _selectedInventorySlot = inventorySlot;

            _highlighter.SetActive(true);
            _highlighter.transform.position = gameObject.transform.position;
            _descriptionManager.Show(inventorySlot);
        }
    }

    public void OnDeselect()
    {
        _selectedInventorySlot = null;

        _highlighter.SetActive(false);
        _descriptionManager.Hide();
    }

    public void OnRemoveButtonClick()
    {
        RemoveItem(_selectedInventorySlot);
    }

    public void OnEquipButtonClick()
    {
        EquipItem(_selectedInventorySlot);
    }

    void RemoveItem(InventorySlot inventorySlot)
    {
        _playerInventory.Remove(inventorySlot.Item);
        Destroy(inventorySlot.gameObject);

        OnDeselect();
    }

    void EquipItem(InventorySlot inventorySlot)
    {
        inventorySlot.Item.Equip(_playerInventory);
    }
}
