using Unity.AppUI.UI;

public class Boots : ItemInstance, IEquippable
{
    public BootsDefinition BootsDefinition => (BootsDefinition)Definition;

    public override string Description
    {
        get
        {
            return $"{BootsDefinition.Description}\n\n" +
                $"Speed Multiplier: {BootsDefinition.SpeedMultiplier}";
        }
    }
    public Boots(ItemDefinition itemDefinition) : base(itemDefinition) { }

    ItemInstance IEquippable.GetInTheInventory(PlayerInventory pi) => pi.Boots;
    void IEquippable.SetInTheInventory(PlayerInventory pi, ItemInstance val) => pi.Boots = val as Boots;

}
