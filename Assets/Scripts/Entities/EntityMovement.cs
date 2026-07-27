using UnityEngine;

public class EntityMovement : MovementBase
{
    [SerializeField] MovementAI _movementAI;

    protected override Vector2 GetMovementIntention()
        => _movementAI.GetMovementIntention(this);
}
