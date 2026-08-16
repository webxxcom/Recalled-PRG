using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] ValueResource _valueResource;
    [SerializeField] Image _topBar;
    [SerializeField] Image _bottomBar;
    [SerializeField] float _animationSpeed;
    [SerializeField] CanvasGroup _canvasGroup;

    public float MaxValue { get; set; }
    public float Value { get; private set; }

    void OnValueChanged(int _, int newValue) => Set(newValue);

    private void OnEnable()
    {
        MaxValue = _valueResource.MaxValue;
        Set(_valueResource.CurrentValue);

        _valueResource.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
        => _valueResource.OnValueChanged -= OnValueChanged;

    IEnumerator ProgressBars(float delta)
    {
        var targetValue = Value / MaxValue;
        var suddenBar = delta <= 0 ? _topBar : _bottomBar;
        var smoothBar = delta <= 0 ? _bottomBar : _topBar;

        _canvasGroup.alpha = 1;
        suddenBar.fillAmount = targetValue;
        while (Mathf.Abs(suddenBar.fillAmount - smoothBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount
                = Mathf.Lerp(smoothBar.fillAmount, suddenBar.fillAmount, Time.deltaTime * _animationSpeed);
            yield return null;
        }
        _canvasGroup.alpha = 0;
        smoothBar.fillAmount = targetValue;
    }

    public void Set(float value)
    {
        float prevValue = Value;
        Value = Mathf.Clamp(value, 0, MaxValue);

        StopAllCoroutines();
        StartCoroutine(ProgressBars(Value - prevValue));
    }
}