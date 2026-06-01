using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [field: SerializeField] public Image TopBar { get; private set; }
    [field: SerializeField] public Image BottomBar { get; private set; }
    [field: SerializeField] public float AnimationSpeed { get; private set; }
    [field: SerializeField] public ValueProvider ValueProvider { get; private set; }
    public float MaxValue { get; set; }
    public float Value { get; private set; }

    private void Start()
    {
        MaxValue = ValueProvider.MaxValue;
        Value = MaxValue;
        ValueProvider.OnValueChanged += (_, value) => Change(value);
    }

    float TargetValue => Value / MaxValue;
    IEnumerator ProgressBars(float value)
    {
        var suddenBar = value > 0 ? BottomBar : TopBar;
        var smoothBar = value > 0 ? TopBar : BottomBar;

        suddenBar.fillAmount = TargetValue;
        while (Mathf.Abs(suddenBar.fillAmount - BottomBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount
                = Mathf.Lerp(smoothBar.fillAmount, suddenBar.fillAmount, Time.deltaTime * AnimationSpeed);
            yield return null;
        }
        smoothBar.fillAmount = TargetValue;
    }

    public void Change(float value)
    {
        Value = Mathf.Clamp(Value + value, 0, MaxValue);
        StartCoroutine(ProgressBars(value));
    }
}