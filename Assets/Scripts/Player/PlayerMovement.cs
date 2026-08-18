using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementBase
{
    [SerializeField] PlayerSprinting _playerSprinting;
    [SerializeField] PlayerDash _playerDash;
    [SerializeField] PlayerCombatData _playerCombat;

    public bool IsSprinting => _playerSprinting.IsActive;

    void OnMove(InputValue value)
        => MovementIntention = value.Get<Vector2>();

    void OnDash(InputValue _)
    {
        if (_playerDash != null) _playerDash.Dash(FacingDirection);
    }

    void OnSprint(InputValue value)
    {
        if (_playerSprinting != null) _playerSprinting.Sprint(value.isPressed);
    }

    protected override Vector2 GetMovementIntention()
    {
        if (!IsWalking)
            return Vector2.zero;

        Vector2 finalMovement = MovementIntention;
        return finalMovement / _playerCombat.Weight;
    }
}
