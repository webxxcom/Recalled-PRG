using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(BlinkingEffectProvider))]
public class InvincibilityProvider : MonoBehaviour
{
    BlinkingEffectProvider _blinkingEffectProvider;
    EntityController _entityController;

    private void Awake()
    {
        _blinkingEffectProvider = GetComponent<BlinkingEffectProvider>();
        _entityController = GetComponent<EntityController>();
    }

    public void Enter()
    {
        _entityController.Health.IsInvincible = true;
        _blinkingEffectProvider.Enter();
    }

    public void Exit()
    {
        _entityController.Health.IsInvincible = false;
        _blinkingEffectProvider.Exit();
    }

    IEnumerator InvincibleCoroutine(float seconds)
    {
        Enter();

        yield return new WaitForSeconds(seconds);

        Exit();
    }

    public void BecomeInvinsibleFor(float seconds)
    {
        StartCoroutine(InvincibleCoroutine(seconds));
    }
}
