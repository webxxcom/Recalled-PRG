using UnityEngine;

public class ChaseMovement : MovementStrategy
{
    [SerializeField] float _minDistanceToTarget;

    public override Vector2 GetDirection(GameObject origin, GameObject target)
    {
        Vector2 diff = target.transform.position - origin.transform.position;

        return diff.sqrMagnitude >= _minDistanceToTarget * _minDistanceToTarget ? diff.normalized : Vector2.zero;
    }

    public override void Init(MovementStrategySO other)
    {
        ChaseMovementSO chaseMovementSO = other as ChaseMovementSO;

        _minDistanceToTarget = chaseMovementSO.MinDistanceToTarget;
    }
    public ChaseMovement(MovementStrategySO other) : base(other) { }
}
