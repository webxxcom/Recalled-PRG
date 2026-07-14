using UnityEngine;

public abstract class EffectDefinition : ScriptableObject
{
    public float duration;

    public abstract void PutOn(EntityController entityController);
    public abstract void Tick(EntityController entityController);
    public abstract void PutOff(EntityController entityController);
}
