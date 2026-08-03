using UnityEngine;

[RequireComponent(typeof(EntityAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    protected MovementBase _movementBase;
    protected EntityAttack _entityAttack;
    protected EntityController _entityController;

    private void Awake()
    {
        _entityAttack = GetComponent<EntityAttack>();

        _entityController = Utils.FindOrThrow(GetComponentInParent<EntityController>);
        _movementBase = Utils.FindOrThrow(GetComponentInParent<MovementBase>);
    }

    public abstract void Execute();
}
