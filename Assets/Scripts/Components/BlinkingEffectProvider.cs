using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class BlinkingEffectProvider : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_1 = new(0.1f);

    EntityController _entityController;
    int _referenceCounter = 0;

    private void Awake()
    {
        _entityController = GetComponent<EntityController>();
    }

    Coroutine blinkingCoroutine;
    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            _entityController.SpriteRendererGroup.SetAlpha(0);
            yield return _waitForSeconds0_1;

            _entityController.SpriteRendererGroup.SetAlpha(1);
            yield return _waitForSeconds0_1;
        }
    }

    public void Enter()
    {
        _referenceCounter++;

        if (blinkingCoroutine != null)
            StopCoroutine(blinkingCoroutine);

        blinkingCoroutine = StartCoroutine(BlinkCoroutine());
    }

    public void Exit()
    {
        _referenceCounter--;

        if (_referenceCounter <= 0)
        {
            _referenceCounter = 0;

            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine);
                blinkingCoroutine = null;
            }

            _entityController.SpriteRendererGroup.SetAlpha(1);
        }
    }
}
