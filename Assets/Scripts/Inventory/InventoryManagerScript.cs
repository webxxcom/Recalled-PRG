using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InventoryManagerScript : MonoBehaviour
{
    [field: SerializeField] GameObject _basicItemsInventoryGrid;
    [field: SerializeField] GameObject _inventoryItemPrefab;
    [field: SerializeField] InventorySlot _swordInventoryItem;
    [field: SerializeField] InventorySlot _armorInventoryItem;
    [field: SerializeField] InventorySlot _bootsInventoryItem;
    [field: SerializeField] GameObject _highlighter;
    [field: SerializeField] DescriptionManager _descriptionManager;
    [SerializeField] InputActionAsset _inputActionAsset;

    EventSystem _eventSystem;
    Canvas _canvas;
    PlayerInput _playerInput;
    PlayerInventory _playerInventory;
    readonly List<GameObject> _createdInventoryItems = new();

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _canvas = Utils.FindOrThrow(GetComponentInChildren<Canvas>);

        _eventSystem = Utils.FindOrThrow(FindAnyObjectByType<EventSystem>);
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

            inventoryItem.GetComponent<InventorySlot>().Initialize(item);

            _createdInventoryItems.Add(inventoryItem);
        }
    }

    void InitBasicItems()
    {
        _swordInventoryItem.Image.sprite = _playerInventory.Sword.Icon;
        _swordInventoryItem.GetComponent<InventorySlot>().Initialize(_playerInventory.Sword);

        _armorInventoryItem.Image.sprite = _playerInventory.Armor.Icon;
        _armorInventoryItem.GetComponent<InventorySlot>().Initialize(_playerInventory.Armor);

        _bootsInventoryItem.Image.sprite = _playerInventory.Boots.Icon;
        _bootsInventoryItem.GetComponent<InventorySlot>().Initialize(_playerInventory.Boots);
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

    private void Update()
    {
        if (_eventSystem.currentSelectedGameObject)
        {
            _highlighter.SetActive(true);
            _highlighter.transform.position = _eventSystem.currentSelectedGameObject.transform.position;
            _descriptionManager.Show(_eventSystem.currentSelectedGameObject.GetComponent<InventorySlot>().Item);
        }
        else if (_highlighter.activeInHierarchy)
        {
            _highlighter.SetActive(false);
            _descriptionManager.Hide();
        }
    }
}
