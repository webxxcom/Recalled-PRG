using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManagerScript : MonoBehaviour
{
    [field: SerializeField] GameObject _basicItemsInventoryGrid;
    [field: SerializeField] GameObject _inventoryItemPrefab;
    [field: SerializeField] InventoryItemScript _swordInventoryItem;
    [field: SerializeField] InventoryItemScript _armorInventoryItem;
    [field: SerializeField] InventoryItemScript _coinInventoryItem;
    [SerializeField] InputActionAsset _inputActionAsset;

    Canvas _canvas;
    PlayerInput _playerInput;
    PlayerInventoryComponent _playerInventory;
    readonly List<GameObject> _createdInventoryItems = new();

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _canvas = Utils.FindOrThrow(GetComponentInChildren<Canvas>);

        _playerInput = Utils.FindOrThrow(FindAnyObjectByType<PlayerInput>);
        _playerInventory = Utils.FindOrThrow(FindAnyObjectByType<PlayerInventoryComponent>);
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
    }

    private void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").FindAction("InventoryToggle").performed -= ToggleInventory;
        _inputActionAsset.FindActionMap("Inventory").FindAction("InventoryToggle").performed -= ToggleInventory;
    }

    void InitGeneralItems()
    {
        foreach (var item in _playerInventory.Items)
        {
            GameObject inventoryItem = Instantiate(_inventoryItemPrefab, Vector3.zero, Quaternion.identity, _basicItemsInventoryGrid.transform);

            inventoryItem.GetComponent<InventoryItemScript>().Initialize(item.Key.Icon, item.Value);

            _createdInventoryItems.Add(inventoryItem);
        }
    }

    void InitBasicItems()
    {
        _coinInventoryItem.CountText.text = _playerInventory.Coins.ToString();

        _swordInventoryItem.Image.sprite = _playerInventory.Sword.Icon;
        _armorInventoryItem.Image.sprite = _playerInventory.Armor.Icon;
    }

    public void Open()
    {
        _canvas.enabled = true;
        _playerInput.SwitchCurrentActionMap("Inventory");
        IsActive = true;

        InitGeneralItems();
        InitBasicItems();
    }

    public void Close()
    {
        _createdInventoryItems.ForEach(ii => Destroy(ii));
        _createdInventoryItems.Clear();

        IsActive = false;
        _canvas.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
    }
}
