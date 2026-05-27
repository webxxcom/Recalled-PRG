using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IAttackStrategy))]
public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] List<HealthComponent> targets;
    [SerializeField] float attackTimeout;

    public GameObject CurrentTarget { get => currentTargets.FirstOrDefault(); }
    public bool IsAttackBlocked { get; set; } = false;

    readonly List<GameObject> currentTargets = new();
    float timeSinceLastAttack;
    IAttackStrategy attackStrategy;

    public event Action OnAttack;

    public void ExecuteAttack()
    {
        attackStrategy.Execute();
    }

    private void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();
    }

    private void Start()
    {
        timeSinceLastAttack = attackTimeout;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthComponent hc)
                && targets.Contains(hc))
        {
            currentTargets.Add(hc.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().TryGetComponent(out HealthComponent hc)
                && targets.Contains(hc))
        {
            currentTargets.Remove(hc.gameObject);
        }
    }

    bool CanAttack() => timeSinceLastAttack >= attackTimeout && !IsAttackBlocked && currentTargets.Count != 0;

    void Attack()
    {
        timeSinceLastAttack = 0;

        OnAttack?.Invoke();
    }

    private void Update()
    {
        if (CanAttack() && attackStrategy.CanBeExecuted())
        {
            Attack();
        }
        timeSinceLastAttack += Time.deltaTime;
    }
}
