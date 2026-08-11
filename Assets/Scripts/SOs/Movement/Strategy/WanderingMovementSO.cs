using UnityEngine;

[CreateAssetMenu(menuName = "Movement/Wandering")]
public class WanderingMovementSO : MovementStrategySO
{
    public override MovementStrategy CreateInstance()
        => new WanderingMovement(this);
}
