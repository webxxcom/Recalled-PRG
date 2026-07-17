using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public class EnemyAttack : EntityAttack
{
    public PlayerController Target
    {
        get
        {
            // TODO do not forget about the attack component
            //HealthProvider ec = TargetsInRange.FirstOrDefault();

            return null;//ec ? ec.GetComponent<PlayerController>() : null;
        }
    }

    AttackStrategy _attackStrategy;

    public override int DealtDamage => BasicAttackData.DealtDamage;
    public override float DealtKnockbackPower => BasicAttackData.KnockbackPower;

    public void ExecuteAttack()
    {
        _attackStrategy.Execute();

        if (Target)
            Effects.ForEach(e => Target.EffectMachineComponent.ApplyEffect(e));
    }

    protected override void Awake()
    {
        base.Awake();

        _attackStrategy = GetComponent<AttackStrategy>();
    }

    private void Start()
    {
        timeSinceLastAttack = ReloadTime;
    }

    void Attack()
    {
        timeSinceLastAttack = 0;

        OnAttackStarted?.Invoke();
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime && Target != null && !Target.Health.IsDead;
    private void Update()
    {
        if (CanAttack)
        {
            Attack();
        }

        timeSinceLastAttack += Time.deltaTime;
    }
}
