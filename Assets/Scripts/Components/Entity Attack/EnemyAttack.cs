using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public class EnemyAttack : EntityAttack
{
    public PlayerController Target
    {
        get
        {
            EntityController ec = TargetsInRange.FirstOrDefault();

            return ec ? ec.GetComponent<PlayerController>() : null;
        }
    }

    AttackStrategy attackStrategy;

    public void ExecuteAttack()
    {
        attackStrategy.Execute();

        if (Target)
            Effects.ForEach(e => Target.EffectMachineComponent.ApplyEffect(e));
    }

    protected override void Awake()
    {
        base.Awake();

        attackStrategy = GetComponent<AttackStrategy>();
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

    bool CanAttack => timeSinceLastAttack >= ReloadTime && Target != null && !Target.HealthComponent.IsDead;
    private void Update()
    {
        if (CanAttack)
        {
            Attack();
        }

        timeSinceLastAttack += Time.deltaTime;
    }
}
