using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(BlinkingEffectComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    BlinkingEffectComponent blinkingEffectComponent;
    EntityController entityController;

    private void Awake()
    {
        blinkingEffectComponent = GetComponent<BlinkingEffectComponent>();
        entityController = GetComponent<EntityController>();
    }

    public void Enter()
    {
        entityController.HealthComponent.IsInvincible = true;
        blinkingEffectComponent.Enter();
    }

    public void Exit()
    {
        entityController.HealthComponent.IsInvincible = false;
        blinkingEffectComponent.Exit();
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
