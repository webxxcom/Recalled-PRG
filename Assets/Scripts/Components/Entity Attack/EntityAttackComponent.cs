using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public class EntityAttackComponent : DefaultAttackComponent
{
    public PlayerController PlayerController
    {
        get
        {
            EntityController ec = TargetsInRange.FirstOrDefault();

            return ec ? ec.GetComponent<PlayerController>() : null;
        }
    }

    float timeSinceLastAttack;
    AttackStrategy attackStrategy;

    public event Action OnAttack;

    public void ExecuteAttack()
    {
        attackStrategy.Execute();

        if (PlayerController)
            Effects.ForEach(e => PlayerController.EffectMachineComponent.ApplyEffect(e));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get Hitbox component and if PlayerController exists in parent then it's the player
        if (collision.TryGetComponent(out HitboxComponent hc))
        {
            PlayerController pc = hc.GetComponentInParent<PlayerController>();

            if (pc)
                TargetsInRange.Add(pc);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitboxComponent hc))
        {
            PlayerController pc = hc.GetComponentInParent<PlayerController>();

            if (pc)
                TargetsInRange.Remove(pc);
        }
    }

    void Attack()
    {
        timeSinceLastAttack = 0;

        OnAttack?.Invoke();
    }

    bool CanAttack => timeSinceLastAttack >= ReloadTime && PlayerController != null && !PlayerController.IsDead;
    private void Update()
    {
        if (CanAttack)
        {
            Attack();
        }

        timeSinceLastAttack += Time.deltaTime;
    }
}
