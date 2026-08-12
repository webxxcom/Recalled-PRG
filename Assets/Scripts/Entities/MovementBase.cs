using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementBase : MonoBehaviour
{
    [SerializeField] float _walkingSpeed;
    [SerializeField] SpeedAggregator _speedAggregator;

    public bool MovementBlocked { get; set; }
    public Vector2 LastMovement { get; set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;

    [Header("Uses")]
    [SerializeField] protected ExternalVelocity _externalVelocity;
    [SerializeField] protected AnimationController _animationController;
    [SerializeField] protected Rigidbody2D _rigidbody2D;

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

    protected abstract Vector2 GetMovementIntention();
    public Vector2 GetFinalMovement()
    {
        if (!isActiveAndEnabled || MovementBlocked)
            return Vector2.zero;

        float speedCoeficient = _speedAggregator != null ? _speedAggregator.Get() : 1f;
        Vector2 externalVelocity = _externalVelocity != null ? _externalVelocity.TickAndGet(Time.fixedDeltaTime) : Vector2.zero;
        Vector2 finalMovement = GetMovementIntention();
        return (speedCoeficient * _walkingSpeed * finalMovement) + externalVelocity;
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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_animationController == null)
            _animationController = GetComponentInChildren<AnimationController>();
    }
#endif
}
