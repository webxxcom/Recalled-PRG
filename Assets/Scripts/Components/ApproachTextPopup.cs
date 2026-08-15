using TMPro;
using UnityEngine;

public class ApproachTextPopup : MonoBehaviour
{
    [SerializeField] string _displayText;
    [SerializeField] TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro.enabled = false;
        _textMeshPro.text = _displayText;
    }

    public void Show()
    {
        _textMeshPro.enabled = true;
    }

    public void Hide()
    {
        _textMeshPro.enabled = false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_textMeshPro == null)
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }
#endif

}
