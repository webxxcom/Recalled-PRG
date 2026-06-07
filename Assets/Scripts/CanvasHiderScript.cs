using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasHiderScript : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    Coroutine hideCoroutine;
    IEnumerator WaitAndHideHealthBar()
    {
        canvasGroup.alpha = 1;

        while (canvasGroup.alpha > 0.1f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * Speed);

            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    public void ShowHealthBar()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(WaitAndHideHealthBar());
    }
}
