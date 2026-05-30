using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(StairMovementComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementComponent : MonoBehaviour
{
    public Vector2 LastMovement { get; private set; }
    public Vector2 MovementIntention { get; private set; }
    public Vector2 FinalMovement => GetFinalMovement();
    public bool IsWalking => MovementIntention != Vector2.zero;
    public bool IsSprinting { get; private set; }
    public float CurrentSpeed => IsSprinting ? SprintingSpeed : WalkingSpeed;
    public Vector2 FacingDirection => MovementIntention != Vector2.zero ? MovementIntention : LastMovement;

    [field: SerializeField] public float SprintingSpeed { get; private set; }
    [field: SerializeField] public float WalkingSpeed { get; private set; }
    [field: SerializeField] public float Stamina { get; private set; }
    [field: SerializeField] public float StaminaUsage { get; private set; }
    [field: SerializeField] public float StaminaRestore { get; private set; }

    float currentStamina;
    float staminaRestoreLastTime = 0;

    StairMovementComponent stairMovementComponent;

    private void Awake()
    {
        stairMovementComponent = GetComponent<StairMovementComponent>();
    }

    void OnMove(InputValue value)
    {
        LastMovement = MovementIntention;
        MovementIntention = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        IsSprinting = value.isPressed;
    }

    void RestoreStaminaWithTime()
    {
        staminaRestoreLastTime += Time.deltaTime;

        bool canRestoreStamina = currentStamina < Stamina
            && !IsSprinting;
        if (staminaRestoreLastTime > 0.3 && canRestoreStamina)
        {
            currentStamina += StaminaRestore;
            staminaRestoreLastTime = 0;
        }
    }

    Vector2 GetFinalMovement()
    {
        Vector2 finalMovement = MovementIntention;

        if (IsSprinting)
        {
            if (currentStamina - StaminaUsage <= 0.3)
                IsSprinting = false;
            else
                currentStamina -= StaminaUsage;
        }

        if (IsWalking)
        {
            finalMovement *= CurrentSpeed;
        }
        else
            finalMovement = Vector2.zero;

        return finalMovement;
    }
    void Update()
    {
        RestoreStaminaWithTime();
    }
}
