using UnityEngine;

public class MeleeAttackComponent : AttackStrategy
{
    public override void Execute()
    {
    }

    private void Update()
    {
        if (!_movementBase.IsWalking)
            return;
        
        float degrees = Vector2.SignedAngle(Vector2.right, _movementBase.MovementIntention);
        _entityAttack.Hitbox.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }
}
