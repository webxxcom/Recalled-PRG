using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] Image _topBar;
    [SerializeField] Image _bottomBar;
    [SerializeField] float _animationSpeed;
    [SerializeField] ValueProvider _valueProvider;

    public float MaxValue { get; set; }
    public float Value { get; private set; }

    void OnValueChanged(GameObject _, int val) => Change(val);

    private void OnEnable()
    {
        MaxValue = _valueProvider.Health.MaxValue;
        Set(_valueProvider.Health.CurrentValue);

        _valueProvider.Health.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _valueProvider.Health.OnValueChanged -= OnValueChanged;
    }

    float TargetValue => Value / MaxValue;
    IEnumerator ProgressBars(float value)
    {
        var suddenBar = value > 0 ? _bottomBar : _topBar;
        var smoothBar = value > 0 ? _topBar : _bottomBar;

        suddenBar.fillAmount = TargetValue;
        while (Mathf.Abs(suddenBar.fillAmount - _bottomBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount
                = Mathf.Lerp(smoothBar.fillAmount, suddenBar.fillAmount, Time.deltaTime * _animationSpeed);
            yield return null;
        }
        smoothBar.fillAmount = TargetValue;
    }

    public void Set(float value)
    {
        Value = Mathf.Clamp(value, 0, MaxValue);
        StartCoroutine(ProgressBars(value));
    }

    public void Change(float value) => Set(Value + value);
}