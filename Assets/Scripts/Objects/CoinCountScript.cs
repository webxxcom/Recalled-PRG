using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasHiderScript))]
public class CoinCountScript : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    CanvasHiderScript canvasHiderScript;

    private void Awake()
    {
        canvasHiderScript = GetComponent<CanvasHiderScript>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeValue(int newValue)
    {
        textMeshProUGUI.SetText(newValue + "");
        canvasHiderScript.ShowCanvas();
    }
}
