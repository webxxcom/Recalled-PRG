using UnityEngine;

/// <summary>
/// EnemyAttack requires detection zone for the enemy to start attacking when their target is in the range
/// </summary>
public class EnemyAttack : EntityAttack
{
    Collider2D currentTarget;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentTarget = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentTarget)
        {
            currentTarget = null;
        }
    }

    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (currentTarget && _timeSinceLastAttack > AttackData.ReloadTime)
        {
            Attack();

            _timeSinceLastAttack = 0;
        }
    }
}
