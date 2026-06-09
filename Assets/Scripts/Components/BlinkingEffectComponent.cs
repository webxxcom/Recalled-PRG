using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class BlinkingEffectComponent : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_2 = new(0.1f);

    EntityController entityController;
    int referenceCounter = 0;

    private void Awake() => entityController = GetComponent<EntityController>();

    Coroutine blinkingCoroutine;
    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            entityController.SpriteRenderer.enabled = false;
            yield return _waitForSeconds0_2;

            entityController.SpriteRenderer.enabled = true;
            yield return _waitForSeconds0_2;
        }
    }

    public void Enter()
    {
        referenceCounter++;

        if (referenceCounter == 1)
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
