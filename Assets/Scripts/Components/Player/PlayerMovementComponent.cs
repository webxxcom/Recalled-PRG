using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StairMovementComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementComponent : MovementBase
{
    public bool IsSprinting { get; private set; }
    public float CurrentSpeed => IsSprinting ? SprintingSpeed : WalkingSpeed;

    [field: SerializeField] public float SprintingSpeed { get; private set; }
    [field: SerializeField] public float Stamina { get; private set; }
    [field: SerializeField] public float StaminaUsage { get; private set; }
    [field: SerializeField] public float StaminaRestore { get; private set; }

    float currentStamina = 100;
    float staminaRestoreLastTime = 0;

    void OnMove(InputValue value)
    {
        LastMovement = MovementIntention;
        MovementIntention = value.Get<Vector2>();
    }

    void OnSprint(InputValue value) => IsSprinting = value.isPressed;

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

    public override Vector2 GetFinalMovement()
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
