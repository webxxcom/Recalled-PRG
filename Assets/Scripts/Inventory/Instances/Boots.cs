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

    public ItemInstance Equip(PlayerInventory playerInventory)
    {
        ItemInstance replaced = playerInventory.Sword;

        playerInventory.Add(playerInventory.Sword);
        playerInventory.Boots = this;
        playerInventory.Remove(this);

        return replaced;
    }
}
