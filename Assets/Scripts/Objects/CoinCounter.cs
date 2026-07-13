using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasHider))]
public class CoinCounter : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    CanvasHider canvasHiderScript;

    private void Awake()
    {
        canvasHiderScript = GetComponent<CanvasHider>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeValue(int newValue)
    {
        textMeshProUGUI.SetText(newValue + "");
        canvasHiderScript.ShowCanvas();
    }
}
