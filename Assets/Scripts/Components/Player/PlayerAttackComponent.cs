using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackComponent : EntityAttackComponent
{
    new Collider2D collider2D;
    PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();

        collider2D = GetComponent<Collider2D>();
        playerController = entityController as PlayerController;
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime;
    void OnAttack(InputValue value)
    {
        if (value.isPressed && CanAttack)
        {
            timeSinceLastAttack = 0;

            OnAttackStarted?.Invoke();
        }
    }

    void SetAttackCollisionOffset()
    {
        if (!playerController.MovementComponent.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, playerController.MovementComponent.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        SetAttackCollisionOffset();
        timeSinceLastAttack += Time.deltaTime;
    }
}
