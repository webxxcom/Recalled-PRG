using UnityEngine;

public abstract class MovementStrategy
{
    public MovementStrategy(MovementStrategySO other) { Init(other); }
    public abstract void Init(MovementStrategySO other);
    public abstract Vector2 GetDirection(GameObject origin, GameObject target);
}
