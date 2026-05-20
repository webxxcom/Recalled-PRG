using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EntityController : MonoBehaviour
{
    // Component attributes
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected StairMovementComponent stairsMovement;

    [SerializeField] protected float speed;
    protected float currentSpeed;

    [SerializeField] protected int health;
    protected int currentHealth;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        stairsMovement = GetComponent<StairMovementComponent>();

        currentSpeed = speed;
        currentHealth = health;
    }

    protected virtual void HandleSpriteFlip(Vector2 dir)
    {
        if (dir.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (dir.x < -0.01f)
            spriteRenderer.flipX = true;
    }

    protected Vector2 ApplyEnvironmentMovement(Vector2 movement)
    {
        if (stairsMovement)
            movement = stairsMovement.ModifyMovementIfOnStairs(movement);

        return movement;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
    }
}
