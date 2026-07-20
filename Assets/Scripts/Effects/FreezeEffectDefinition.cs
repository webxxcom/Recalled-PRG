using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Freeze")]
public class FreezeEffectDefinition : EffectDefinition
{
    static readonly float SpeedMultiplier = 0.4f;
    static readonly Color Color = new(0.5f, 1f, 1f);

    public override void PutOn(EntityController entityController)
    {
        entityController.SpriteRendererGroup.SetColor(Color);

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(SpeedMultiplier);
    }

    public override void PutOff(EntityController entityController)
    {
        entityController.SpriteRendererGroup.SetColor(Color.white);

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(SpeedMultiplier);
    }

    public override void Tick(EntityController entityController)
    {
    }
}
