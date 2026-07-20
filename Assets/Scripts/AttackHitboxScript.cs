using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitboxScript : MonoBehaviour
{
    EntityController entityController;
    EnemyAttack entityAttackComponent;
    new Collider2D collider2D;

    private void Awake()
    {
        entityAttackComponent = GetComponentInParent<EnemyAttack>();
        entityController = GetComponentInParent<EntityController>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
