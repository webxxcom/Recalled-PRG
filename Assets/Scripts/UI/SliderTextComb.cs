using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextComb : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    Slider _slider;

    private void Awake()
        => _slider = GetComponentInChildren<Slider>();
    private void Start()
        => OnValChanged(_slider.value);
    private void OnEnable()
        => _slider.onValueChanged.AddListener(OnValChanged);
    private void OnDisable()
        => _slider.onValueChanged.RemoveListener(OnValChanged);

    void OnValChanged(float val)
    {
        _text.text = $"{Mathf.RoundToInt(val * 100)}/100";
    }
}
