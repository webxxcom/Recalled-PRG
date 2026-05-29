using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileScript : MonoBehaviour
{
    [field: SerializeField] public float AdvancingSpeed { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float TimeToLive { get; private set; }

    public Vector2 Destination { get; set; }
    public EntityController Owner { get; set; }

    float livingTime = 0;
    Vector2 direction;
    new Rigidbody2D rigidbody2D;
    new Collider2D collider2D;

    void InitRotation()
    {
        direction = (Destination - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0,0, angle);
    }

    public void Initialize(EntityController owner, Vector2 destination)
    {
        Destination = destination;
        Owner = owner;

        InitRotation();
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = direction * AdvancingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Owner.gameObject && collision.gameObject.TryGetComponent(out HealthComponent hc))
        {
            Debug.Log("Arrow just hit " + collision.gameObject.name);
            hc.TakeDamage(Owner.gameObject, DealtDamage, KnockbackPower);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (livingTime >= TimeToLive)
        {
            Destroy(gameObject);
            return;
        }

        livingTime += Time.deltaTime;
    }
}
