using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] HealthProvider _valueProvider;
    [SerializeField] Image _topBar;
    [SerializeField] Image _bottomBar;
    [SerializeField] float _animationSpeed;
    [SerializeField] CanvasGroup _canvasGroup;

    public float MaxValue { get; set; }
    public float Value { get; private set; }

    public void Init(HealthProvider healthProvider)
    {
        _valueProvider = healthProvider;

        enabled = true;
    }

    void OnValueChanged(DamageInfo damageInfo) => Change(damageInfo.Amount);

    private void OnEnable()
    {
        if (_valueProvider)
        {
            MaxValue = _valueProvider.MaxValue;
            Set(_valueProvider.CurrentValue);

            _valueProvider.OnValueChanged += OnValueChanged;
        }
    }

    private void OnDisable()
    {
        if (_valueProvider)
            _valueProvider.OnValueChanged -= OnValueChanged;
    }

    float TargetValue => Value / MaxValue;
    IEnumerator ProgressBars(float value)
    {
        var suddenBar = value <= 0 ? _topBar : _bottomBar;
        var smoothBar = value <= 0 ? _bottomBar : _topBar;

        if (_canvasGroup)
            _canvasGroup.alpha = 1;
        suddenBar.fillAmount = TargetValue;
        while (Mathf.Abs(suddenBar.fillAmount - smoothBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount
                = Mathf.Lerp(smoothBar.fillAmount, suddenBar.fillAmount, Time.deltaTime * _animationSpeed);
            yield return null;
        }
        if (_canvasGroup)
            _canvasGroup.alpha = 0;
        smoothBar.fillAmount = TargetValue;
    }

    public void Set(float value)
    {
        float prevValue = Value;
        Value = Mathf.Clamp(value, 0, MaxValue);

        StopAllCoroutines();
        StartCoroutine(ProgressBars(Value - prevValue));
    }

    public void Change(float value) => Set(Value + value);
}