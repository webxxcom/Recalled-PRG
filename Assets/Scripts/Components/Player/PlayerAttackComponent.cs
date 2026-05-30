using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackComponent : MonoBehaviour
{
    [field: SerializeField] public float AttackReloadTime { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }

    float timeSinceLastAttack;

    readonly List<HealthComponent> inRange = new();
    new Collider2D collider2D;
    PlayerMovementComponent playerMovementComponent;
    Animator animator;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
        playerMovementComponent = GetComponentInParent<PlayerMovementComponent>();
    }

    bool CanAttack => timeSinceLastAttack >= AttackReloadTime;
    void OnAttack(InputValue value)
    {
        if (value.isPressed && CanAttack)
        {
            inRange.ForEach(d => d.TakeDamage(gameObject, DealtDamage, KnockbackPower));
            timeSinceLastAttack = 0;
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent healthComponent = collision.GetComponentInParent<HealthComponent>();

        if (healthComponent)
        {
            inRange.Add(healthComponent);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HealthComponent healthComponent = collision.GetComponentInParent<HealthComponent>();
        
        if (healthComponent)
        {
            inRange.Remove(healthComponent);
        }
    }

    void SetAttackCollisionOffset()
    {
        if (!playerMovementComponent.IsWalking)
            return;

        float degrees = Vector2.SignedAngle(Vector2.right, playerMovementComponent.MovementIntention);
        collider2D.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        SetAttackCollisionOffset();
        timeSinceLastAttack += Time.deltaTime;
    }
}
