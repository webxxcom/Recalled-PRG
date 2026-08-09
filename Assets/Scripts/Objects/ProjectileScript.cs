using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileScript : MonoBehaviour
{
    [field: SerializeField] public float AdvancingSpeed { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float TimeToLive { get; private set; }
    [field: SerializeField] Vector2 _offset;

    Vector3 _direction;
    GameObject _owner;
    Rigidbody2D _rigidbody2D;
    Collider2D _collider2D;

    public void Initialize(GameObject owner, Vector3 destination)
    {
        _owner = owner;
        Vector3 pos = (Vector2)transform.position + _offset;
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
        if (!collision.CompareTag(_owner.tag) && collision.TryGetComponent(out HealthProvider hp))
        {
            // TODO the hurtbox and effects
            hp.DealDamage(new(DealtDamage, KnockbackPower, _owner, _collider2D, hp.Hurtbox, null));
        }

        Destroy(gameObject);
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
