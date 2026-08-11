using UnityEngine;

public abstract class MovementStrategySO : ScriptableObject
{
    public abstract MovementStrategy CreateInstance();
}
