public class Sword : ItemInstance, IEquippable
{
    public SwordDefinition SwordDefinition => (SwordDefinition)Definition;

    public override string Description
    {
        get
        {
            return $"{SwordDefinition.Description}\n\n" +
                $"Power: {SwordDefinition.Damage}\n" +
                $"Knockback Power: {SwordDefinition.KnockbackPower}\n" +
                $"Weight: {SwordDefinition.Weight}";
        }
    }
    public Sword(ItemDefinition itemDefinition) : base(itemDefinition) { }

    public ItemInstance Equip(PlayerInventory playerInventory)
    {
        ItemInstance replaced = playerInventory.Sword;

        playerInventory.Add(playerInventory.Sword);
        playerInventory.Sword = this;
        playerInventory.Remove(this);

        return replaced;
    }
}
