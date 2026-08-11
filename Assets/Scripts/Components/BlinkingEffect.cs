using System.Collections;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] float _blinkInterval;
    [SerializeField] SpriteRendererGroup _spriteRendererGroup;
    [SerializeField] HealthResource _healthProvider;

    private void OnEnable() => _healthProvider.OnValueChanged += OnValueChanged;
    private void OnDisable() => _healthProvider.OnValueChanged -= OnValueChanged;
    void OnValueChanged(int _, int _1) => StartBlinking();

    IEnumerator BlinkCoroutine()
    {
        float elapsed = 0;
        while (elapsed < _duration)
        {
            _spriteRendererGroup.SetAlpha(0);
            yield return new WaitForSeconds(_blinkInterval);

            _spriteRendererGroup.SetAlpha(1);
            yield return new WaitForSeconds(_blinkInterval);

            elapsed += _blinkInterval * 2;
        }
        _spriteRendererGroup.SetAlpha(1);
    }

    public void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine(BlinkCoroutine());
    }
}
