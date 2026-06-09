using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire")]
public class FireEffect : EffectAsset
{
    public int DamagePerSecond = 2;

    public override void PutOn(EntityController entityController)
    {
        entityController.SpriteRenderer.color =
            new Color(0.5f, 1f, 1f);

        entityController.MovementBase.SpeedMultiplier *= 0.4f;
    }

    public override void PutOff(EntityController entityController)
    {
        entityController.SpriteRenderer.color =
            Color.white;

        entityController.MovementBase.SpeedMultiplier /= 0.4f;
    }

    float timeSinceDamage = 0;
    readonly float reloadTime = 0.5f;
    public override void Update(EntityController entityController)
    {
        if (timeSinceDamage > reloadTime)
        {
            entityController.HealthComponent.Change(null, -DamagePerSecond);
            timeSinceDamage = 0;
        }

        timeSinceDamage += Time.deltaTime;
    }
}
