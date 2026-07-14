using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class BlinkingEffectProvider : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_1 = new(0.1f);

    EntityController entityController;
    int referenceCounter = 0;

    private void Awake() => entityController = GetComponent<EntityController>();

    Coroutine blinkingCoroutine;
    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            entityController.SpriteRenderer.enabled = false;
            yield return _waitForSeconds0_1;

            entityController.SpriteRenderer.enabled = true;
            yield return _waitForSeconds0_1;
        }
    }

    public void Enter()
    {
        referenceCounter++;

        if (blinkingCoroutine != null)
            StopCoroutine(blinkingCoroutine);

        blinkingCoroutine = StartCoroutine(BlinkCoroutine());
    }

    public void Exit()
    {
        referenceCounter--;

        if (referenceCounter <= 0)
        {
            referenceCounter = 0;

            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine);
                blinkingCoroutine = null;
            }

            entityController.SpriteRenderer.enabled = true;
        }
    }
}
