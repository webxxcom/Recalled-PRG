using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class EffectMachineComponent : MonoBehaviour
{
    readonly HashSet<EffectAsset> activeEffects = new();

    EntityController entityController;

    private void Awake()
    {
        entityController = GetComponent<EntityController>();
    }

    public void ApplyEffect(EffectAsset effect)
    {
        StartCoroutine(ApplyCoroutine(effect));
    }

    IEnumerator ApplyCoroutine(EffectAsset effect)
    {
        effect.PutOn(entityController);

        float timeSinceStart = 0;
        while (timeSinceStart < effect.duration)
        {
            effect.Update(entityController);
            timeSinceStart += Time.deltaTime;

            yield return null;
        }

        effect.PutOff(entityController);
    }
}
