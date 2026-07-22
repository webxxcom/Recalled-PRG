using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Player Attack Data")]
public class PlayerAttackData : AttackData
{
    [SerializeField] InventorySO _inventory;

    public override int DealtDamage
    {
        get
        {
            int totalDamage = base.DealtDamage;
            if (_inventory.Sword != null)
                totalDamage += _inventory.Sword.Definition.Damage;
            return totalDamage;
        }
    }

    public override float KnockbackPower
    {
        get
        {
            float totalKnockback = base.KnockbackPower;
            if (_inventory.Sword != null)
                totalKnockback *= _inventory.Sword.Definition.KnockbackPower;
            return totalKnockback;
        }
    }
    // TODO figure out what to do with the sword weight
    public override float ReloadTime => base.ReloadTime;
}
