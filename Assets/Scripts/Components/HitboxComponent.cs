using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    new Collider2D collider2D;
    HealthComponent healthComponent;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        healthComponent = GetComponentInParent<HealthComponent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out ProjectileScript ps))
        {
            healthComponent.TakeDamage(collision.gameObject, ps.GetDealtDamage(), ps.GetKnockbackPower());
        }
    }
}
