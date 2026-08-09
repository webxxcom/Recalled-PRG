using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : EntityAttack
{
    void OnAttack(InputValue value)
    {
        //TODO player attack strategy
        if (value.isPressed && _timeSinceLastAttack >= _attackStrategies[0].AttackData.ReloadTime)
        {
            _timeSinceLastAttack = 0;

            Attack(_attackStrategies[0]);
        }
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }
}
