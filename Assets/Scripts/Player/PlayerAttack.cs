using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : EntityAttack
{
    void OnAttack(InputValue value)
    {
        if (value.isPressed && _timeSinceLastAttack >= AttackData.ReloadTime)
        {
            _timeSinceLastAttack = 0;

            Attack();
        }
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }
}
