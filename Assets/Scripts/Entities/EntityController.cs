using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public abstract class EntityController : MonoBehaviour
{
    // Component attributes
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    new protected Rigidbody2D rigidbody2D;
    protected StairMovementComponent stairsMovement;
    protected HealthComponent healthComponent;

    [field: SerializeField] public bool IsDead { get; set; }
    [field: SerializeField] public bool IsFrozen { get; set; }
    [field: SerializeField] public bool IsStatic { get; set; }

    void OnDeath()
    {
        IsDead = true;
        rigidbody2D.linearVelocity = Vector2.zero;
        animator.SetTrigger("Death");
    }

    protected void PlayHurtAnimation(string triggerName = "Hurt")
    {
        animator.SetTrigger(triggerName);
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        stairsMovement = GetComponent<StairMovementComponent>();
        healthComponent = GetComponent<HealthComponent>();
    }

    protected virtual void Start()
    {
        healthComponent.OnDeath += OnDeath;
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

    private void FixedUpdate()
    {
        if (IsFrozen || IsDead || IsStatic)
            return;

        HandleFixedUpdate();
    }

    protected virtual void HandleFixedUpdate()
    {
    }

    public void FreezeFor(float seconds)
    {
        StartCoroutine(FrozenCoroutine(seconds));
    }

    IEnumerator FrozenCoroutine(float seconds)
    {
        Freeze();

        yield return new WaitForSeconds(seconds);

        UnFreeze();
    }

    void Freeze()
    {
        IsFrozen = true;
        rigidbody2D.linearVelocity *= 0.4f;
    }

    void UnFreeze() => IsFrozen = false;
}
