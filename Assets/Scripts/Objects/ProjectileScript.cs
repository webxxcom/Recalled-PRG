using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileScript : MonoBehaviour
{
    [field: SerializeField] public float AdvancingSpeed { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float TimeToLive { get; private set; }

    public Vector2 Direction { get; private set; }

    new Rigidbody2D rigidbody2D;

    void InitRotation()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0,0, angle);
    }

    public void Initialize(string owner, Vector2 destination)
    {
        Direction = destination;

        InitRotation();
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = Direction * AdvancingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.TryGetComponent(out HealthProvider hc) && collision.gameObject != Owner.gameObject)
        //{
        //    hc.DealDamage(Owner.gameObject, DealtDamage);
        //    Destroy(gameObject);
        //}
    }

    float elapsedLivingTime = 0;
    private void Update()
    {
        if (elapsedLivingTime >= TimeToLive)
        {
            Destroy(gameObject);
            return;
        }

        elapsedLivingTime += Time.deltaTime;
    }
}
