using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementBase : MonoBehaviour
{
    [SerializeField] float _walkingSpeed;

    public bool MovementBlocked { get; set; }
    public AggregatedValue SpeedAggregator { get; private set; } = new();
    public Vector2 LastMovement { get; set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public float CurrentSpeed => _walkingSpeed * SpeedAggregator.Get();
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;
   
    [SerializeField] ExternalVelocity _externalVelocity;
    [SerializeField] AnimationController _animationController;

    protected Rigidbody2D _rigidbody2D;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    Vector2 _movementIntention;
    public Vector2 MovementIntention
    {
        get => _movementIntention;
        protected set
        {
            if (_movementIntention != Vector2.zero)
                LastMovement = _movementIntention;

            var _prevMov = _movementIntention;
            _movementIntention = value;

            if (_prevMov == Vector2.zero && value != Vector2.zero)
                OnMovementStarted?.Invoke();
            else if (_prevMov != Vector2.zero && value == Vector2.zero)
                OnMovementStopped?.Invoke();

            if (value != Vector2.zero)
                OnMovement?.Invoke();
        }
    }

    public event Action OnMovementStarted;
    public event Action OnMovementStopped;
    public event Action OnMovement;

    public void AddExternalVelocity(Vector2 vec) => _externalVelocity.Add(vec);

    protected abstract Vector2 GetMovementIntention();
    public Vector2 GetFinalMovement()
    {
        if (!isActiveAndEnabled || MovementBlocked)
            return Vector2.zero;

        Vector2 finalMovement = GetMovementIntention();

        return (SpeedAggregator.Get() * _walkingSpeed * finalMovement)
            + _externalVelocity.TickAndGet(Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Vector2 finalMovement = GetFinalMovement();

        if (finalMovement != Vector2.zero)
            _rigidbody2D.linearVelocity = finalMovement;
        else
            _rigidbody2D.linearVelocity *= 0.9f;

        _animationController.MoveAnimation(FacingDirection,
            _rigidbody2D.linearVelocity.magnitude / 4f);
    }
}
