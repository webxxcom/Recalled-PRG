using UnityEngine;
using System.Collections;

public interface IMovementStrategy
{
    public Vector2 GetDirection(GameObject target);
}
