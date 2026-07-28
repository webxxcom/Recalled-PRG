using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class Invincibility : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] HealthProvider _healthProvider;

    private void OnEnable() => _healthProvider.OnHpChanged += OnHpChanged;
    private void OnDisable() => _healthProvider.OnHpChanged -= OnHpChanged;
    private void OnHpChanged(DamageInfo _) => BecomeInvinsibleFor();

    IEnumerator InvincibleCoroutine()
    {
        _healthProvider.IsInvincible = true;

        yield return new WaitForSeconds(_duration);

        _healthProvider.IsInvincible = false;
    }

    public void BecomeInvinsibleFor()
    {
        StopAllCoroutines();
        StartCoroutine(InvincibleCoroutine());
    }
}
