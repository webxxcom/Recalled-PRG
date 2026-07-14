using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    protected Collider2D _collider2D;
    protected MovementBase _movementBase;
    protected EnemyAttack _entityAttack;
    protected EntityController _entityController;

    private void Awake()
    {
        _entityAttack = GetComponent<EnemyAttack>();
        _collider2D = GetComponent<Collider2D>();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
        _movementBase = Utils.FindOrThrow(GetComponentInParent<MovementBase>);
    }

    public abstract void Execute();
}
