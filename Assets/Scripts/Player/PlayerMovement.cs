using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementBase
{
    [System.Serializable]
    public class SprintingProperty
    {
        [SerializeField] public float _stamina;
        [SerializeField] public float _staminaUsage;
        [SerializeField] public float _staminaRestore;
        [field: SerializeField] public float SpeedMultiplier { get; private set; }

        float _staminaRestoreLastTime = 0;
        float _currentStamina = 100;

        public void RestoreStaminaWithTime(bool isSprinting)
        {
            _staminaRestoreLastTime += Time.deltaTime;

            bool canRestoreStamina = _currentStamina < _stamina && !isSprinting;
            if (_staminaRestoreLastTime > 0.3 && canRestoreStamina)
            {
                _currentStamina += _staminaRestore;
                _staminaRestoreLastTime = 0;
            }
        }

        public bool ProcessSprintingState(bool isSprinting)
        {
            // If not sprinting or have no enough stamina we're not sprinting
            if (!isSprinting || _currentStamina - _staminaUsage <= 0.3)
                return false;

            // Otherwise subtract stamina usage
            _currentStamina -= _staminaUsage;
            return true;
        }
    }

    [field: SerializeField] public SprintingProperty SprintingState { get; private set; }
    [field: SerializeField] public float DashReloadTime { get; private set; }
    [field: SerializeField] public float DashForce { get; private set; }
    [SerializeField] Invincibility _invincibility;

    public bool IsSprinting { get; private set; }

    void OnMove(InputValue value)
    {
        MovementIntention = value.Get<Vector2>();
    }

    void OnDash(InputValue _)
    {
        AddExternalVelocity(FacingDirection * DashForce);
        _invincibility.BecomeInvinsibleFor(0.3f);
    }

    void OnSprint(InputValue value)
    {
        if (IsSprinting && value.isPressed)
            return;

        IsSprinting = value.isPressed;
        if (IsSprinting)
            SpeedAggregator.Add(SprintingState.SpeedMultiplier);
        else
            SpeedAggregator.Remove(SprintingState.SpeedMultiplier);
    }

    protected override Vector2 GetMovementIntention()
    {
        if (!IsWalking)
            return Vector2.zero;

        Vector2 finalMovement = MovementIntention;

        SprintingState.ProcessSprintingState(IsSprinting);
        return finalMovement;
    }
    void Update()
    {
        SprintingState.RestoreStaminaWithTime(IsSprinting);
    }
}
