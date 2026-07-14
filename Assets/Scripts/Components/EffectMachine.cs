using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class EffectMachine : MonoBehaviour
{
    readonly HashSet<EffectDefinition> activeEffects = new();

    EntityController _entityController;

    private void Awake()
    {
        _entityController = GetComponent<EntityController>();
    }

    public void ApplyEffect(EffectDefinition effect)
    {
        StartCoroutine(ApplyCoroutine(effect));
    }

    IEnumerator ApplyCoroutine(EffectDefinition effect)
    {
        effect.PutOn(_entityController);

        float timeSinceStart = 0;
        while (timeSinceStart < effect.duration)
        {
            effect.Tick(_entityController);
            timeSinceStart += Time.deltaTime;

            yield return null;
        }

        effect.PutOff(_entityController);
    }
}
