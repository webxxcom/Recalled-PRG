using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire")]
public class FireEffectDefinition : EffectDefinition
{
    static readonly int DamagePerSecond = 2;
    static readonly float SpeedMultiplier = 0.8f;
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

    float timeSinceDamage = 0;
    readonly float reloadTime = 0.5f;
    public override void Tick(EntityController entityController)
    {
        if (timeSinceDamage > reloadTime)
        {
            entityController.Health.Change(null, -DamagePerSecond);
            timeSinceDamage = 0;
        }

        timeSinceDamage += Time.deltaTime;
    }
}
