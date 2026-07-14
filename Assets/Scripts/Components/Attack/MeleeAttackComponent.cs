using UnityEngine;

public class MeleeAttackComponent : AttackStrategy
{
    public override void Execute()
    {
        PlayerController target = _entityAttack.Target;

        if (target)
        {
            _entityAttack.DealDamage(target.Health, _entityController.gameObject);
        }
    }

    private void Update()
    {
        if (!_movementBase.IsWalking)
            return;
        
        float degrees = Vector2.SignedAngle(Vector2.right, _movementBase.MovementIntention);
        _collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }
}
