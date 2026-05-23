using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(GameObject attacker, int damage, float knockbackPower);
}
