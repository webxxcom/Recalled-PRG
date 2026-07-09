using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasHider : MonoBehaviour
{
    [field: SerializeField] public float Offset { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = Utils.FindOrThrow(GetComponent<CanvasGroup>);
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    Coroutine hideCoroutine;
    IEnumerator WaitAndHideCanvas()
    {
        while (canvasGroup.alpha < 0.95f)
        {
            canvasGroup.alpha += Time.deltaTime * 10;

            yield return null;
        }
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(Offset);

        while (canvasGroup.alpha > 0.1f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * Speed);

            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    public void ShowCanvas()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(WaitAndHideCanvas());
    }
}
