using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class EnemyController : EntityController
{
    public EntityMovement MovementComponent { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        MovementComponent = GetComponent<EntityMovement>();
    }
}
