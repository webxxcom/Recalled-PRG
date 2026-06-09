using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultAttackComponent : MonoBehaviour
{
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public List<EffectAsset> Effects { get; private set; }
}
