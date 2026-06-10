using UnityEngine;

[RequireComponent(typeof(EntityMovementComponent))]
public abstract class MovementStrategy : MonoBehaviour
{
    public abstract Vector2 GetDirection(GameObject target);
}
