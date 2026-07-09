using TMPro;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    public bool IsActive { get; private set; }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(InventoryItem inventoryItem)
    {
        if (!IsActive)
        {
            IsActive = true;
            gameObject.SetActive(true);
        }
        _text.text = inventoryItem.Description;
    }

    public void Hide()
    {
        IsActive = false;
        gameObject.SetActive(false);
        _text.text = null;
    }
}
