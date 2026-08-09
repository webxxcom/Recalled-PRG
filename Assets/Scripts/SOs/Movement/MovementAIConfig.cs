using UnityEngine;

[CreateAssetMenu(menuName = "Movement / MovementAIConfig")]
public class MovementAIConfig : ScriptableObject
{
    [Header("Idle movement")]
    [field: SerializeField] public MovementStrategySO IdleMovementStrategy { get; private set; }

    [Header("If target is present Movement")]
    [field: SerializeField] public MovementStrategySO[] MovementStrategies { get; private set; }
    [field: SerializeField] public TargetProviderSO[] TargetProviders { get; private set; }
}
