using UnityEngine;

[CreateAssetMenu(menuName = "Movements/Wandering")]
public class WanderingMovementSO : MovementStrategySO
{
    public override MovementStrategy CreateInstance()
        => new WanderingMovement(this);
}
