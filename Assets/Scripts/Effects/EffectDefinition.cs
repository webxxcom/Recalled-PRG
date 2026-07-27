using UnityEngine;

public abstract class EffectDefinition : ScriptableObject
{
    [field: SerializeField] public float Duration { get; private set; }

    public abstract void PutOn(EntityController entityController);
    public abstract void Tick(HealthProvider health);
    public abstract void PutOff(EntityController entityController);
}
