using UnityEngine;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract Vector2 GetDirection(GameObject origin, GameObject target, out bool reachedDestination);
}
