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
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public HealthProvider Health { get; private set; }
    public Collider2D Collider2D { get; private set; }

    void OnDeath()
    {
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

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Health = GetComponent<HealthProvider>();
        Collider2D = GetComponent<Collider2D>();

        Animator = Utils.GetComponentInChildrenIfNotPresent<Animator>(gameObject);
        SpriteRenderer = Utils.GetComponentInChildrenIfNotPresent<SpriteRenderer>(gameObject);
    }

    protected virtual void Start()
    {
        Health.OnMinValueReached += obj => OnDeath();
        Health.OnValueChanged += (_, value) => { if (value <= 0) OnHurt(); };
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
    }

    private void FixedUpdate()
    {
        if (Health.IsDead)
            return;

        HandleFixedUpdate();
    }

    protected virtual void HandleFixedUpdate()
    {
    }
}
