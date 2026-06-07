using UnityEngine;

public abstract class DefaultAttackComponent : MonoBehaviour
{
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public int DealtDamage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
}
