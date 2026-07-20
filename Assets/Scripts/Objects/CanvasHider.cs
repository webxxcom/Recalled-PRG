using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasHider : MonoBehaviour
{
    [field: SerializeField] public float Offset { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = Utils.FindOrThrow(GetComponent<CanvasGroup>);
    }

    private void Start()
    {
        _canvasGroup.alpha = 0;
    }

    Coroutine hideCoroutine;
    IEnumerator WaitAndHideCanvas()
    {
        while (_canvasGroup.alpha < 0.95f)
        {
            _canvasGroup.alpha += Time.deltaTime * 10;

            yield return null;
        }
        _canvasGroup.alpha = 1;

        yield return new WaitForSeconds(Offset);

        while (_canvasGroup.alpha > 0.1f)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 0, Time.deltaTime * Speed);

            yield return null;
        }
        _canvasGroup.alpha = 0;
    }

    public void ShowCanvas()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(WaitAndHideCanvas());
    }
}
