using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementBase))]
[RequireComponent(typeof(Collider2D))]
public abstract class EntityController : MonoBehaviour
{
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DieHash = Animator.StringToHash("Die");

    // Component attributes
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public Collider2D Collider2D { get; private set; }

    [field: SerializeField] public bool IsDead { get; set; }

    void OnDeath()
    {
        IsDead = true;
        Rigidbody2D.bodyType = RigidbodyType2D.Static;
        GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(s => s.sortingOrder = -1);
        SpriteRenderer.sortingOrder = -1;
        Collider2D.enabled = false;
        Animator.SetTrigger(DieHash);
        Animator.SetFloat(MovementBase.SpeedHash, 0);

        foreach (var c in GetComponentsInChildren<MonoBehaviour>())
            c.enabled = false;
    }

    void OnHurt()
    {
        Animator.SetTrigger(HurtHash);
    }

    T GetComponentInChildrenIfNotPresent<T>()
    {
        return TryGetComponent(out T component) ? component : GetComponentInChildren<T>();
    }

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        HealthComponent = GetComponent<HealthComponent>();
        Collider2D = GetComponent<Collider2D>();

        Animator = GetComponentInChildrenIfNotPresent<Animator>();
        SpriteRenderer = GetComponentInChildrenIfNotPresent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        HealthComponent.OnMinValueReached += obj => OnDeath();
        HealthComponent.OnValueChanged += (_, value) => { if (value <= 0) OnHurt(); };
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
    }

    protected virtual void HandleFixedUpdate()
    {
    }
}
