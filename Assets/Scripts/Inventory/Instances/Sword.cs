public class Sword : ItemInstance
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
    public override bool IsEquippable => true;

    public override ItemInstance CreateInstance(ItemDefinition itemDefinition) => new Sword(itemDefinition);

    public Sword(ItemDefinition itemDefinition) : base(itemDefinition) { }

    public override void Equip(PlayerInventory playerInventory)
    {
        playerInventory.Add(playerInventory.Sword);
        playerInventory.Sword = this;
        playerInventory.Remove(this);
    }
}
