using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Freeze")]
public class FreezeEffect : EffectAsset
{
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

    public override void Update(EntityController entityController)
    {
    }
}
