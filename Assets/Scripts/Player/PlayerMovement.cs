using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementBase
{
    [field: SerializeField] public float SprintingSpeedMultiplier { get; private set; }
    [field: SerializeField] public float Stamina { get; private set; }
    [field: SerializeField] public float StaminaUsage { get; private set; }
    [field: SerializeField] public float StaminaRestore { get; private set; }

    public bool IsSprinting { get; private set; }

    float _currentStamina = 100;
    float _staminaRestoreLastTime = 0;

    void OnMove(InputValue value)
    {
        LastMovement = MovementIntention;
        MovementIntention = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        // If we already sprint but the unpress event was not called
        if (IsSprinting && value.isPressed)
            return;

        IsSprinting = value.isPressed;

        if (IsSprinting)
            SpeedAggregator.Add(SprintingSpeedMultiplier);
        else
            SpeedAggregator.Remove(SprintingSpeedMultiplier);
    }

    void RestoreStaminaWithTime()
    {
        _staminaRestoreLastTime += Time.deltaTime;

        bool canRestoreStamina = _currentStamina < Stamina && !IsSprinting;
        if (_staminaRestoreLastTime > 0.3 && canRestoreStamina)
        {
            _currentStamina += StaminaRestore;
            _staminaRestoreLastTime = 0;
        }
    }

    protected override Vector2 GetMovementIntention()
    {
        Vector2 finalMovement = MovementIntention;

        if (IsSprinting)
        {
            if (_currentStamina - StaminaUsage <= 0.3)
                IsSprinting = false;
            else
                _currentStamina -= StaminaUsage;
        }

        if (IsWalking)
        {
            finalMovement *= WalkingSpeed;
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
