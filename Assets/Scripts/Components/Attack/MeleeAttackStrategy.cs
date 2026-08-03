using UnityEngine;

public class MeleeAttackStrategy : AttackStrategy
{
    public override void Execute()
    {
    }

    private void OnEnable()
    {
        _movementBase.OnMovement += SetAttackCollisionOffset;
    }

    private void OnDisable()
    {
        _movementBase.OnMovement -= SetAttackCollisionOffset;
    }

    void SetAttackCollisionOffset()
    {
        _entityAttack.Hitbox.transform.rotation
            = Quaternion.FromToRotation(Vector2.right, _movementBase.MovementIntention);
    }
}
