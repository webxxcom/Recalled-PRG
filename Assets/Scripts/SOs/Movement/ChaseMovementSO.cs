using UnityEngine;

[CreateAssetMenu(menuName = "Movement/Chasing")]
public class ChaseMovementSO : MovementStrategySO
{
    [field: SerializeField] public float MinDistanceToTarget { get; private set; }

    public override MovementStrategy CreateInstance()
        => new ChaseMovement(this);
}
