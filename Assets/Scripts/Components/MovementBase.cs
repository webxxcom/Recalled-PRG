using System;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    [field: SerializeField] public float WalkingSpeed { get; protected set; }

    public float SpeedMultiplier { get; set; } = 1f;
    public Vector2 LastMovement { get; protected set; }
    public Vector2 MovementIntention { get; protected set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;

    public abstract Vector2 GetFinalMovement();

    private void OnDisable() => MovementIntention = Vector2.zero;
}
