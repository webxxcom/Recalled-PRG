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

    public override ItemInstance CreateInstance(ItemDefinition itemDefinition) => new Sword(itemDefinition);

    public Sword(ItemDefinition itemDefinition) : base(itemDefinition) { }
}
