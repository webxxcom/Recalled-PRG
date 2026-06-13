using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackComponent : DefaultAttackComponent
{
    float timeSinceLastAttack;

    new Collider2D collider2D;

    protected override void Awake()
    {
        base.Awake();

        collider2D = GetComponent<Collider2D>();
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime;
    void OnAttack(InputValue value)
    {
        if (value.isPressed && CanAttack)
        {
            timeSinceLastAttack = 0;
            entityController.Animator.SetTrigger("Attack");
            OnAttackEvent?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            EntityController entityController = collision.GetComponentInParent<EntityController>();

            if (entityController && !entityController.IsDead)
                TargetsInRange.Add(entityController);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent _))
        {
            TargetsInRange.Remove(collision.GetComponentInParent<EntityController>());
        }
    }

    void SetAttackCollisionOffset()
    {
        if (!entityController.MovementBase.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, entityController.MovementBase.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        SetAttackCollisionOffset();
        timeSinceLastAttack += Time.deltaTime;
    }
}
