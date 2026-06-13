using System.Collections;
using System.Linq;
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
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public Collider2D Collider2D { get; private set; }
    public MovementBase MovementBase { get; set; }

    [field: SerializeField] public bool IsDead { get; set; }

    void OnDeath()
    {
        IsDead = true;
        Rigidbody2D.bodyType = RigidbodyType2D.Static;
        GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(s => s.sortingOrder = -1);
        SpriteRenderer.sortingOrder = -1;
        Collider2D.enabled = false;
        Animator.SetTrigger(DieHash);
        Animator.SetFloat(SpeedHash, 0);

        foreach (var c in GetComponentsInChildren<MonoBehaviour>())
            c.enabled = false;
    }

    void OnHurt()
    {
        Animator.SetTrigger(HurtHash);
    }

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        HealthComponent = GetComponent<HealthComponent>();
        MovementBase = GetComponent<MovementBase>();
        Collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        HealthComponent.OnMinValueReached += obj => OnDeath();
        HealthComponent.OnValueChanged += (_, value) => { if (value < 0) OnHurt(); };
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
    }

    private void FixedUpdate()
    {
        if (IsDead)
            return;

        HandleFixedUpdate();
        
        if (MovementBase)
        {
            Animator.SetFloat(MoveXHash, MovementBase.FacingDirection.x);
            Animator.SetFloat(MoveYHash, MovementBase.FacingDirection.y);
        }

        Animator.SetFloat(SpeedHash, Rigidbody2D.linearVelocity.magnitude / 3);
    }

    protected virtual void HandleFixedUpdate()
    {
    }
}
