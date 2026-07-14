using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    protected new Collider2D collider2D;
    protected MovementBase movementBase;
    protected EnemyAttack entityAttackComponent;
    protected EntityController entityController;

    private void Awake()
    {
        entityController = GetComponentInParent<EntityController>();
        movementBase = GetComponentInParent<MovementBase>();
        entityAttackComponent = GetComponent<EnemyAttack>();
        collider2D = GetComponent<Collider2D>();
    }

    public void SetAttackCollisionOffset()
    {
        if (!movementBase.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, movementBase.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    public abstract void Execute();
}
