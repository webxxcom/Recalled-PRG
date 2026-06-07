using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitboxScript : MonoBehaviour
{
    EntityController entityController;
    EntityAttackComponent entityAttackComponent;
    new Collider2D collider2D;

    private void Awake()
    {
        entityAttackComponent = GetComponentInParent<EntityAttackComponent>();
        entityController = GetComponentInParent<EntityController>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HitboxComponent hitboxComponent))
        {
            hitboxComponent.HealthComponent
                .Change(entityController.gameObject, -entityAttackComponent.DealtDamage);
        }
    }
}
