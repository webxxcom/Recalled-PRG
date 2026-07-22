using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthProvider))]
[RequireComponent(typeof(Collider2D))]
public abstract class EntityController : MonoBehaviour
{
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DieHash = Animator.StringToHash("Die");

    // Component attributes
    public Animator Animator { get; private set; }
    public SpriteRendererGroup SpriteRendererGroup { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public HealthProvider HealthProvider { get; private set; }
    public Collider2D Collider2D { get; private set; }

    void OnDeath(GameObject _)
    {
        Rigidbody2D.bodyType = RigidbodyType2D.Static;
        Animator.SetTrigger(DieHash);
    }

    void OnHpValueChanged(GameObject _, int damage)
    {
        if (damage < 0)
            Animator.SetTrigger(HurtHash);
    }

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        HealthProvider = GetComponent<HealthProvider>();
        Collider2D = GetComponent<Collider2D>();

        Animator = Utils.GetComponentInChildrenIfNotPresent<Animator>(gameObject);
        SpriteRendererGroup = GetComponentInChildren<SpriteRendererGroup>();
    }

    protected virtual void Start() { }

    protected virtual void OnEnable()
    {
        HealthProvider.Health.OnMinValueReached += OnDeath;
        HealthProvider.Health.OnValueChanged += OnHpValueChanged;
    }

    protected virtual void OnDisable()
    {
        HealthProvider.Health.OnMinValueReached += OnDeath;
        HealthProvider.Health.OnValueChanged += OnHpValueChanged;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
    protected virtual void OnTriggerExit2D(Collider2D collision) { }

    private void FixedUpdate()
    {
        if (HealthProvider.IsDead)
            return;

        HandleFixedUpdate();
    }

    protected virtual void HandleFixedUpdate() { }
}
