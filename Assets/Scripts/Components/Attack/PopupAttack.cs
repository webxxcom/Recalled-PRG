public class PopupAttack : AttackStrategy
{
    public override void Execute()
    {
        PlayerController target = _entityAttack.Target;

        if (target)
        {
            _entityAttack.DealDamage(target.Health, _entityController.gameObject);
        }
    }
}
