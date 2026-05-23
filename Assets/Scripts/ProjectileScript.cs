using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float advancingSpeed;
    [SerializeField] int dealtDamage;   
    [SerializeField] float knockbackPower;

    new Rigidbody2D rigidbody2D;
    public Vector2 StartPos { get; set; }
    public Vector2 Destination { get; set; }
    public EntityController Owner { get; set; }

    Vector2 direction;

    public int GetDealtDamage() => dealtDamage;
    public float GetKnockbackPower() => knockbackPower;

    void InitRotation()
    {
        direction = (Destination - StartPos).normalized;
        float angle;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.Rotate(0,0, angle);
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.position = StartPos;
        InitRotation();
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = direction * advancingSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
