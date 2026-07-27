using UnityEngine;

[CreateAssetMenu(menuName = "Attack / Attack Curves Data")]
public class AttackCurvesSO : ScriptableObject
{
    [field: SerializeField] public AnimationCurve ColliderSizeX { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderSizeY { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderOffsetX { get; private set; }
    [field: SerializeField] public AnimationCurve ColliderOffsetY { get; private set; }
}
