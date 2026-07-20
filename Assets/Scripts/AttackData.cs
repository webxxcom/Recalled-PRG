using UnityEngine;

[CreateAssetMenu(menuName = "Attack Data")]
public class AttackData : ScriptableObject
{
    [field: SerializeField] public int DealtDamage { get; set; } = 10;
    [field: SerializeField] public float ReloadTime { get; set; } = 0.8f;
    [field: SerializeField] public float KnockbackPower { get; set; } = 1.6f;
    [field: SerializeField] public float ImpactTime { get; private set; } = 0.3f;
    [field: SerializeField] public float RecoveryTime { get; private set; } = 0.8f;
    [field: SerializeField] public float SpeedMultiplier { get; private set; } = 0.3f;
    [field: SerializeField] public AnimationCurve ColliderSizeX { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderSizeY { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderOffsetX { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderOffsetY { get; private set; }
}
