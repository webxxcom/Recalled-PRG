using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScriptUI : MonoBehaviour
{
    [Tooltip("Do not set if used with Init method")]
    [SerializeField] IntVariable _valueVariable;
    [SerializeField] Image _bar;
    [SerializeField] float _animationSpeed;

    public float MaxValue { get; set; }
    public float Value { get; private set; }

    public void Init(IntVariable intVariable, int max)
    {
        _valueVariable = intVariable;
        MaxValue = max;

        enabled = true;
    }

    void OnValueChanged(int newValue) => Set(newValue);

    private void OnEnable()
    {
        Set(_valueVariable.Value);

        _valueVariable.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
        => _valueVariable.OnValueChanged -= OnValueChanged;

    IEnumerator ProgressBars()
    {
        float targetValue = Value / MaxValue;
        while (_bar.fillAmount - targetValue > float.Epsilon)
        {
            _bar.fillAmount
                = Mathf.Lerp(_bar.fillAmount, targetValue, Time.deltaTime * _animationSpeed);
            yield return null;
        }
        _bar.fillAmount = targetValue;
    }

    public void Set(float value)
    {
        Value = Mathf.Clamp(value, 0, MaxValue);

        StopAllCoroutines();
        StartCoroutine(ProgressBars());
    }
}