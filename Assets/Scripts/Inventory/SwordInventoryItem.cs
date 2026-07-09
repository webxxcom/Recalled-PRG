using UnityEngine;

[CreateAssetMenu(fileName = "SwordInventoryItem", menuName = "Inventory Items/Sword")]
public class SwordInventoryItem : InventoryItem
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float Weight { get; private set; }
}
