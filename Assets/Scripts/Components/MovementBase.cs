using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementBase : MonoBehaviour
{
    public static readonly int MoveYHash = Animator.StringToHash("MoveY");
    public static readonly int MoveXHash = Animator.StringToHash("MoveX");
    public static readonly int SpeedHash = Animator.StringToHash("Speed");

    [field: SerializeField] public float WalkingSpeed { get; protected set; }

    public bool MovementBlocked { get; set; }
    public AggregatedValue SpeedAggregator { get; private set; } = new();
    public Vector2 LastMovement { get; set; }
    public Vector2 PrevMovement { get; protected set; }
    public bool IsWalking => MovementIntention != Vector2.zero;
    public float CurrentSpeed => WalkingSpeed * SpeedAggregator.Get();
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;
   
    [SerializeField] Animator _animator;
    ExternalVelocitySO _externalVelocity;
    protected Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _externalVelocity = ScriptableObject.CreateInstance<ExternalVelocitySO>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Vector2 MovementIntention
    {
        get => _movementIntention;
        set
        {
            _movementIntention = value;

            if (_movementIntention == Vector2.zero && value != Vector2.zero)
                OnMovementStarted?.Invoke();
            else if (_movementIntention != Vector2.zero && value == Vector2.zero)
                OnMovementStopped?.Invoke();
            else
                OnMovement?.Invoke();
        }
    }

    Vector2 _movementIntention;

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

        return (SpeedAggregator.Get() * WalkingSpeed * finalMovement)
            + _externalVelocity.TickAndGet(Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        PrevMovement = MovementIntention;
        
        Vector2 finalMovement = GetFinalMovement();

        if (finalMovement != Vector2.zero)
            _rigidbody2D.linearVelocity = finalMovement;
        else
            _rigidbody2D.linearVelocity *= 0.9f;

        _animator.SetFloat(MoveXHash, Mathf.Abs(FacingDirection.x) > 0.01f ? FacingDirection.x : 0f);
        _animator.SetFloat(MoveYHash, Mathf.Abs(FacingDirection.x) < 0.01f ? FacingDirection.y : 0f);
        _animator.SetFloat(SpeedHash, _rigidbody2D.linearVelocity.magnitude / 4f);
    }
}
