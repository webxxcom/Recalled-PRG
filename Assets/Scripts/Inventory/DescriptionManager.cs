using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _mainText;
    [SerializeField] GameObject _buttons;

    [SerializeField] GameObject _equipButton;
    [SerializeField] GameObject _removeButton;

    public bool IsActive => gameObject.activeInHierarchy;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void ShowButtons(InventorySlot inventorySlot)
    {
        HideButtons();

        if (inventorySlot.Item.IsEquippable)
            _equipButton.SetActive(true);

        if (inventorySlot.IsRemovable)
            _removeButton.SetActive(true);
    }

    void HideButtons()
    {
        _equipButton.SetActive(false);
        _removeButton.SetActive(false);
    }

    public void Show(InventorySlot inventorySlot)
    {
        if (!IsActive)
        {
            gameObject.SetActive(true);
        }
        _mainText.text = inventorySlot.Item.Description;

        ShowButtons(inventorySlot);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _mainText.text = null;
        HideButtons();
    }
}
