using UnityEngine;

[CreateAssetMenu(fileName = "ArmorDefinition", menuName = "Inventory Items/Armor")]
public class ArmorDefinition : ItemDefinition
{
    [field: SerializeField] public float Protection { get; private set; }
    [field: SerializeField] public float Weight { get; private set; }
}
