using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IAttackStrategy))]
public class EntityAttackComponent: MonoBehaviour
{
    [field: SerializeField] public List<HealthComponent> Targets { get; private set; }
    [field: SerializeField] public float AttackTimeout { get; private set; }

    public GameObject CurrentTarget => PriorityTarget ? PriorityTarget : CurrentAvailableTarget;
    public GameObject PriorityTarget { get; private set; }
    public GameObject CurrentAvailableTarget { get => currentTargets.FirstOrDefault(); }
    public bool IsAttackBlocked { get; set; } = false;

    readonly List<GameObject> currentTargets = new();
    float timeSinceLastAttack;
    IAttackStrategy attackStrategy;
    AgressionComponent aggressionComponent;

    public event Action OnAttack;

    public void ExecuteAttack()
    {
        attackStrategy.Execute();
    }

    private void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();

        aggressionComponent = GetComponentInParent<AgressionComponent>();
    }

    private void Start()
    {
        timeSinceLastAttack = AttackTimeout;
    }

    private bool IsPriorityTarget(Collider2D collision, out EntityController entityController)
    {
        // Dependency on the fact that Hitbox must always be an entity's child no matter what !

        entityController = collision.GetComponentInParent<EntityController>();

        return entityController
                && entityController.gameObject == aggressionComponent.CurrentTarget;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsPriorityTarget(collision, out EntityController entityController))
        {
            PriorityTarget = entityController.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthComponent health)
            && Targets.Contains(health))
        {
            currentTargets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPriorityTarget(collision, out _))
        {
            PriorityTarget = null;
        }

        if (collision.TryGetComponent(out HealthComponent health)
            && Targets.Contains(health))
        {
            currentTargets.Remove(collision.gameObject);
        }
    }

    void Attack()
    {
        timeSinceLastAttack = 0;

        OnAttack?.Invoke();
    }

    // TODO what the hell is happening with these triggers
    bool CanAttack => timeSinceLastAttack >= AttackTimeout && !IsAttackBlocked && CurrentTarget != null
            && CurrentTarget.TryGetComponent(out EntityController ec) && !ec.IsDead;
    private void Update()
    {
        if (CanAttack)
        {
            Attack();
        }
        timeSinceLastAttack += Time.deltaTime;
    }
}
