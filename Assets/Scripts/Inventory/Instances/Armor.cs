public class Armor : ItemInstance, IEquippable
{
    public ArmorDefinition ArmorDefinition => (ArmorDefinition)Definition;

    public override string Description
    {
        get => $"{ArmorDefinition.Description}\n\n" +
                $"Protection: {ArmorDefinition.Protection}\n" +
                $"Weight: {ArmorDefinition.Weight}";
    }

    public Armor(ItemDefinition itemDefinition) : base(itemDefinition) { }

    public ItemInstance Equip(PlayerInventory playerInventory)
    {
        ItemInstance replaced = playerInventory.Sword;

        playerInventory.Add(playerInventory.Sword);
        playerInventory.Armor = this;
        playerInventory.Remove(this);

        return replaced;
    }
}
