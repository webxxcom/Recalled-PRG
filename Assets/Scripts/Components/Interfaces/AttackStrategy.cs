using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EntityAttackComponent))]
public abstract class AttackStrategy : MonoBehaviour
{
    protected new Collider2D collider2D;
    protected EntityController entityController;
    protected EntityAttackComponent entityAttackComponent;

    private void Awake()
    {
        entityController = GetComponentInParent<EntityController>();
        entityAttackComponent = GetComponent<EntityAttackComponent>();
        collider2D = GetComponent<Collider2D>();
    }

    public void SetAttackCollisionOffset()
    {
        if (!entityController.MovementBase.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, entityController.MovementBase.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    public abstract void Execute();
}
