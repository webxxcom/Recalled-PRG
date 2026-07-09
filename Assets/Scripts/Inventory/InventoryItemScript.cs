using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemScript : MonoBehaviour
{
    public Image Image { get; private set; }
    public TextMeshProUGUI CountText { get; private set; }

    private void Awake()
    {
        Image = GetComponent<Image>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(Sprite image)
    {
        Image.sprite = image;
        CountText.text = null;
    }

    public void Initialize(Sprite image, int count)
    {
        if (!image)
            Image.color = new Color(1, 1, 1, 0);
        else
            Image.sprite = image;

        CountText.text = count != 1 ? count.ToString() : null;
    }
}
