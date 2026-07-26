using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class InvincibilityProvider : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] HealthProvider _healthProvider;

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
