using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] float attackReloadTime;
    [SerializeField] int dealtDamage;
    [SerializeField] float knockbackPower;

    float timeSinceLastAttack;

    List<IDamageable> inRange = new();
    Animator animator;
    new Collider2D collider2D;
    float colliderOffset;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();

        if (!collider2D.isTrigger)
            Debug.LogError("The collider2D for AttackComponent must be a trigger");
    }

    private void Start()
    {
        colliderOffset = collider2D.offset.x;
    }

    bool CanAttack() => timeSinceLastAttack >= attackReloadTime;

    private void OnAttack(InputValue value)
    {
        if (value.isPressed && CanAttack())
        {
            animator.SetTrigger("isInteracting");

            inRange.ForEach(d => d.TakeDamage(gameObject, dealtDamage, knockbackPower));
            timeSinceLastAttack = 0;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 mov = value.Get<Vector2>();

        if (mov.x < -0.01f)
        {
            collider2D.offset = new(-colliderOffset, collider2D.offset.y);
        }
        else if (mov.x > 0.01f)
        {
            collider2D.offset = new(colliderOffset, collider2D.offset.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable obj))
        {
            inRange.Add(obj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable obj))
        {
            inRange.Remove(obj);
        }
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }
}
