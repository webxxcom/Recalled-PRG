using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMachineSO : ScriptableObject
{
    readonly List<EffectDefinition> _activeEffects = new();

    public void ApplyEffect(EntityController entityController, HealthProvider health, EffectDefinition effect)
    {
        entityController.StartCoroutine(ApplyCoroutine(entityController, health, effect));
    }

    IEnumerator ApplyCoroutine(EntityController entityController, HealthProvider health, EffectDefinition effect)
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
