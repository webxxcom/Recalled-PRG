using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    [field: SerializeField] public float WalkingSpeed { get; protected set; }

    public bool MovementBlocked { get; set; }
    public AggregatedValue SpeedAggregator { get; private set; } = new();
    public Vector2 LastMovement { get; protected set; }
    public Vector2 MovementIntention { get; protected set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;

    public event Action OnMovement;

    protected abstract Vector2 GetMovementIntention();

    public Vector2 GetFinalMovement()
    {
        if (!enabled)
            return Vector2.zero;

        Vector2 finalMovement = GetMovementIntention();

        if (finalMovement != Vector2.zero)
            OnMovement?.Invoke();

        return finalMovement * SpeedAggregator.Get();
    }

    private void OnDisable() => MovementIntention = Vector2.zero;
}
