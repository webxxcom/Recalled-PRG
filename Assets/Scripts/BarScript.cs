using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] Image topBar;
    [SerializeField] Image bottomBar;
    [SerializeField] float animationSpeed;
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int Value { get; private set; }

    float TargetValue => (float)Value / MaxValue;

    IEnumerator ProgressBottomBar(int amount)
    {
        var suddenMoveBar = amount > 0 ? topBar : bottomBar;
        var smoothBar = amount > 0 ? bottomBar : topBar;

        while (Mathf.Abs(smoothBar.fillAmount - suddenMoveBar.fillAmount) > 0.01f)
        {
            smoothBar.fillAmount = Mathf.Lerp(
                smoothBar.fillAmount,
                suddenMoveBar.fillAmount,
                Time.deltaTime * animationSpeed);
           
            yield return null;
        }
        smoothBar.fillAmount = topBar.fillAmount;
    }

    public void SetMax(int val)
    {
        MaxValue = val;

        SetCurrent(Value);
    }

    public void Change(int amount)
    {
        SetCurrent(Value + amount);
    }

    public void SetCurrent(int val)
    {
        Value = Mathf.Clamp(val, 0, MaxValue);

        topBar.fillAmount = TargetValue;
        StartCoroutine(ProgressBottomBar(val));
    }
}
