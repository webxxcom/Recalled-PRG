using UnityEngine;

public abstract class MovementStrategy
{
    public abstract MovementStrategy Init(MovementStrategySO other);
    public abstract Vector2 GetDirection(GameObject origin, GameObject target, out bool reachedDestination);
}
