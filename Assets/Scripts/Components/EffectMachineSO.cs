using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMachineSO : ScriptableObject
{
    readonly Dictionary<EffectDefinition, Coroutine> _activeEffects = new();

    public void ApplyEffect(EntityController entityController, HealthResource health, EffectDefinition effect)
    {
        if (_activeEffects.ContainsKey(effect))
            entityController.StopCoroutine(_activeEffects[effect]);

        _activeEffects[effect] = entityController.StartCoroutine(ApplyCoroutine(entityController, health, effect));
    }

    IEnumerator ApplyCoroutine(EntityController entityController, HealthResource health, EffectDefinition effect)
    {
        effect.PutOn(entityController);

        float timeSinceStart = 0;
        while (timeSinceStart < effect.Duration)
        {
            effect.Tick(health);
            timeSinceStart += Time.deltaTime;

            yield return null;
        }

        effect.PutOff(entityController);
    }
}
