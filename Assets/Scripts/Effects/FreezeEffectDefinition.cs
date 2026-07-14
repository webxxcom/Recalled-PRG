using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Freeze")]
public class FreezeEffectDefinition : EffectDefinition
{
    readonly float speedMultiplier = 0.4f;

    public override void PutOn(EntityController entityController)
    {
        entityController.SpriteRenderer.color = new(0.5f, 1f, 1f);

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    public override void PutOff(EntityController entityController)
    {
        entityController.SpriteRenderer.color = Color.white;

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    public override void Tick(EntityController entityController)
    {
    }
}
