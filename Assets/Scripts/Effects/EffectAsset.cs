using UnityEngine;

public abstract class EffectAsset : ScriptableObject
{
    public float duration;

    public abstract void PutOn(EntityController entityController);
    public abstract void Update(EntityController entityController);
    public abstract void PutOff(EntityController entityController);
}
