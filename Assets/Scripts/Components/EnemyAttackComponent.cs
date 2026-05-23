using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttackStrategy))]
[RequireComponent(typeof(Collider2D))]
public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] List<HealthComponent> targets;
    [SerializeField] float attackTimeout;

    public GameObject CurrentTarget { get => currentTargets.Peek(); }

    IAttackStrategy attackStrategy;
    new Collider2D collider2D;

    Queue<GameObject> currentTargets = new();
    float timeSinceLastAttack;
    public bool IsAttackBlocked { get; set; } = false;

    public event Action OnAttack;

    private void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        timeSinceLastAttack = attackTimeout;
    }

    bool CanAttack() => timeSinceLastAttack >= attackTimeout && !IsAttackBlocked && currentTargets.Count != 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthComponent hc)
                && targets.Contains(hc))
        {
            currentTargets.Enqueue(hc.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider>().TryGetComponent(out HealthComponent hc)
                && targets.Contains(hc))
        {
            currentTargets.Dequeue();
        }
    }

    private void Update()
    {
        if (CanAttack() && attackStrategy.CanBeExecuted())
        {
            timeSinceLastAttack = 0;

            attackStrategy.Execute();
            OnAttack?.Invoke();
        }
        timeSinceLastAttack += Time.deltaTime;
    }
}
