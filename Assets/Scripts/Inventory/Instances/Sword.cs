using Unity.VisualScripting;

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

    ItemInstance IEquippable.GetInTheInventory(PlayerInventory pi) => pi.Sword;
    void IEquippable.SetInTheInventory(PlayerInventory pi, ItemInstance val) =>  pi.Sword = val as Sword;
}
