using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileScript : MonoBehaviour
{
    [field: SerializeField] public float AdvancingSpeed { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float TimeToLive { get; private set; }
    [SerializeField] Vector2 _offset;

    Vector3 _direction;
    GameObject _owner;
    Rigidbody2D _rigidbody2D;
    Collider2D _collider2D;

    public void Initialize(GameObject owner, Vector3 destination, bool flipX)
    {
        _owner = owner;
        Vector3 pos = (Vector2)transform.position + new Vector2(_offset.x * (flipX ? -1 : 1), _offset.y);

        _direction = (destination - pos).normalized;
        transform.SetPositionAndRotation(
            pos,
            Quaternion.FromToRotation(Vector3.right, _direction));
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _direction * AdvancingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_owner.tag))
            return;

        if (collision.TryGetComponent(out HealthProvider hp))
        {
            hp.DealDamage(new(DealtDamage, KnockbackPower, _owner, _collider2D, hp.Hurtbox, null));
        }
        Destroy(gameObject);
    }

    float _elapsedLivingTime;
    private void Update()
    {
        if (_elapsedLivingTime >= TimeToLive)
        {
            Destroy(gameObject);
            return;
        }

        _elapsedLivingTime += Time.deltaTime;
    }
}
