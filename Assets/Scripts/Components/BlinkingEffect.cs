using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class BlinkingEffect : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] float _blinkInterval;
    [SerializeField] SpriteRendererGroup _spriteRendererGroup;

    IEnumerator BlinkCoroutine()
    {
        float elapsed = 0;
        while (elapsed < _duration)
        {
            _spriteRendererGroup.gameObject.SetActive
                (!_spriteRendererGroup.gameObject.activeInHierarchy);
            yield return new WaitForSeconds(_blinkInterval);

            elapsed += Time.deltaTime;
        }
        _spriteRendererGroup.gameObject.SetActive(true);
    }

    public void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine(BlinkCoroutine());
    }
}
