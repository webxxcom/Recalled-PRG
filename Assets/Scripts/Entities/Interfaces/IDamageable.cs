using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public interface IDamageable
{
    public GameObject gameObject { get; }

    public void TakeDamage(GameObject attacker, int damage, float knockbackPower);
}
