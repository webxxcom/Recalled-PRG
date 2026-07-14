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

    ItemInstance IEquippable.GetInTheInventory(PlayerInventory pi) => pi.Armor;
    void IEquippable.SetInTheInventory(PlayerInventory pi, ItemInstance val) => pi.Armor = val as Armor;
}
