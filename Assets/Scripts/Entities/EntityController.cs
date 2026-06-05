using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementBase))]
[RequireComponent(typeof(Collider2D))]
public abstract class EntityController : MonoBehaviour
{
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");

    // Component attributes
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    new protected Rigidbody2D rigidbody2D;
    protected HealthComponent healthComponent;
    new public Collider2D collider2D { get; protected set; }
    protected MovementBase MovementBase { get; set; }

    [field: SerializeField] public bool IsDead { get; set; }
    [field: SerializeField] public bool IsFrozen { get; set; }

    void OnDeath()
    {
        IsDead = true;
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        spriteRenderer.sortingOrder = -1;
        collider2D.enabled = false;
        animator.SetTrigger(DieHash);
    }

    void OnHurt() => animator.SetTrigger(HurtHash);

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        MovementBase = GetComponent<MovementBase>();
        collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        healthComponent.OnMinValueReached += obj => OnDeath();
        healthComponent.OnValueChanged += (_, value) => { if (value < 0) OnHurt(); };
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
        if (IsFrozen || IsDead)
        {
            if (!IsDead)
                rigidbody2D.linearVelocity *= 0.8f;
            return;
        }

        HandleFixedUpdate();
        animator.SetFloat(MoveXHash, MovementBase.FacingDirection.x);
        animator.SetFloat(MoveYHash, MovementBase.FacingDirection.y);
        animator.SetFloat(SpeedHash, rigidbody2D.linearVelocity.magnitude / 3);
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

    public void Freeze()
    {
        IsFrozen = true;
        rigidbody2D.linearVelocity *= 0.4f;
    }

    public void UnFreeze() => IsFrozen = false;
}
