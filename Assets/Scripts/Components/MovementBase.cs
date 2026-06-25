using System;
using UnityEngine;

[RequireComponent(typeof(ExternalVelocityComponent))]
public abstract class MovementBase : MonoBehaviour
{
    public static readonly int MoveYHash = Animator.StringToHash("MoveY");
    public static readonly int MoveXHash = Animator.StringToHash("MoveX");
    public static readonly int SpeedHash = Animator.StringToHash("Speed");

    [field: SerializeField] public float WalkingSpeed { get; protected set; }

    public bool MovementBlocked { get; set; }
    public AggregatedValue SpeedAggregator { get; private set; } = new();
    public Vector2 LastMovement { get; protected set; }
    public Vector2 PrevMovement { get; protected set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public float CurrentSpeed => WalkingSpeed * SpeedAggregator.Get();
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;
   
    ExternalVelocityComponent externalVelocityComponent;

    private void Awake()
    {
        externalVelocityComponent = GetComponent<ExternalVelocityComponent>();
    }

    public Vector2 MovementIntention
    {
        get => _movementIntention;
        protected set
        {
            if (_movementIntention == Vector2.zero && value != Vector2.zero)
                OnMovementStarted?.Invoke();
            else if (_movementIntention != Vector2.zero && value == Vector2.zero)
                OnMovementStopped?.Invoke();
            _movementIntention = value;
        }
    }

    Vector2 _movementIntention;

    public event Action OnMovementStarted;
    public event Action OnMovementStopped;

    protected abstract Vector2 GetMovementIntention();

    public Vector2 GetFinalMovement()
    {
        if (!enabled || MovementBlocked)
            return Vector2.zero;

        Vector2 finalMovement = GetMovementIntention();

        return (finalMovement * SpeedAggregator.Get())
            + (externalVelocityComponent != null ? externalVelocityComponent.TickAndGet(Time.fixedDeltaTime) : Vector2.zero);
    }

    private void OnDisable() => MovementIntention = Vector2.zero;

    private void FixedUpdate()
    {
        PrevMovement = MovementIntention;
    }
}
