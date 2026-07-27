using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire")]
public class FireEffectDefinition : EffectDefinition
{
    [SerializeField] int _damagePerSecond = 2;
    [SerializeField] float SpeedMultiplier = 0.8f;
    [SerializeField] float reloadTime = 0.5f;
    static readonly Color Color = new(0.7f, 0.1f, 0.1f);

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
            movementBase.SpeedAggregator.Remove(SpeedMultiplier);
    }

    float timeSinceDamage = 0;
    public override void Tick(HealthProvider health)
    {
        if (timeSinceDamage > reloadTime)
        {
            health.DealDamage(null, _damagePerSecond);
            timeSinceDamage = 0;
        }

        timeSinceDamage += Time.deltaTime;
    }
}
