using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileScript : MonoBehaviour
{
    [field: SerializeField] public float AdvancingSpeed { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float TimeToLive { get; private set; }

    public Vector2 Destination { get; private set; }
    public EntityController Owner { get; private set; }

    float totalLivingTime = 0;
    Vector2 direction;
    new Rigidbody2D rigidbody2D;

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
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = direction * AdvancingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthComponent hc) && collision.gameObject != Owner.gameObject)
        {
            hc.Change(Owner.gameObject, -DealtDamage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (totalLivingTime >= TimeToLive)
        {
            Destroy(gameObject);
            return;
        }

        totalLivingTime += Time.deltaTime;
    }
}
