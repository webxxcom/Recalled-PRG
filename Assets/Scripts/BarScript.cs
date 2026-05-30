using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] Image topBar;
    [SerializeField] Image bottomBar;
    [field: SerializeField] public float AnimationSpeed { get; private set; }
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public ValueProvider ValueProvider { get; private set; }

    float TargetFill => (float)Value / MaxValue;

    IEnumerator ProgressBars(int amount)
    {
        var suddenMoveBar = amount > 0 ? topBar : bottomBar;
        var smoothBar = amount > 0 ? bottomBar : topBar;

        suddenMoveBar.fillAmount = TargetFill;
        while (Mathf.Abs(smoothBar.fillAmount - suddenMoveBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount = Mathf.Lerp(
                smoothBar.fillAmount,
                suddenMoveBar.fillAmount,
                Time.deltaTime * AnimationSpeed);
           
            yield return null;
        }
        smoothBar.fillAmount = topBar.fillAmount;
    }

    private void Start()
    {
        SetMax(ValueProvider.MaxValue);

        ValueProvider.OnValueChanged += (obj, val) => Change(val);
    }

    public void SetMax(int val)
    {
        MaxValue = val;

        SetCurrent(val);
    }

    public void Change(int amount)
    {
        SetCurrent(Value + amount);
    }

    public void SetCurrent(int val)
    {
        Value = Mathf.Clamp(val, 0, MaxValue);

        StartCoroutine(ProgressBars(val));
    }
}
