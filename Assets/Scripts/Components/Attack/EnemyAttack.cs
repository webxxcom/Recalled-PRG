using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// EnemyAttack requires detection zone for the enemy to start attacking when their target is in the range
/// </summary>
public class EnemyAttack : EntityAttack
{
    bool _wantsAttack;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _wantsAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _wantsAttack = false;
    }

    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (_wantsAttack && _timeSinceLastAttack > AttackData.ReloadTime)
        {
            Attack();

            _timeSinceLastAttack = 0;
        }
    }
}
