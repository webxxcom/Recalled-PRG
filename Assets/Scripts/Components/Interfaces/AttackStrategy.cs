using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    protected MovementBase _movementBase;
    protected EnemyAttack _entityAttack;
    protected EntityController _entityController;

    private void Awake()
    {
        _entityAttack = GetComponent<EnemyAttack>();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
        _movementBase = Utils.FindOrThrow(GetComponentInParent<MovementBase>);
    }

    public abstract void Execute();
}
