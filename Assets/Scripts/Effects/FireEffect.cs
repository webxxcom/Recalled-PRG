using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire")]
public class FireEffect : EffectAsset
{
    readonly int damagePerSecond = 2;
    readonly float speedMultiplier = 0.8f;

    public override void PutOn(EntityController entityController)
    {
        entityController.SpriteRenderer.color = new Color(0.5f, 1f, 1f);

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    public override void PutOff(EntityController entityController)
    {
        entityController.SpriteRenderer.color = Color.white;

        if (entityController.TryGetComponent(out MovementBase movementBase))
            movementBase.SpeedAggregator.Add(speedMultiplier);
    }

    float timeSinceDamage = 0;
    readonly float reloadTime = 0.5f;
    public override void Tick(EntityController entityController)
    {
        if (timeSinceDamage > reloadTime)
        {
            entityController.HealthComponent.Change(null, -damagePerSecond);
            timeSinceDamage = 0;
        }

        timeSinceDamage += Time.deltaTime;
    }
}
