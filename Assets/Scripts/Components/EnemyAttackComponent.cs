using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttackStrategy))]
public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] List<HealthComponent> targets;
    [SerializeField] int dealtDamage;
    [SerializeField] float attackTimeout;

    Animator animator;
    EntityController entityController;
    IAttackStrategy attackStrategy;
    float timeSinceLastAttack;
    public bool IsAttackBlocked { get; set; } = false;

    public event Action OnAttack;

    private void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();
        animator = GetComponentInParent<Animator>();
        entityController = GetComponentInParent<EntityController>();
    }

    private void Start()
    {
        timeSinceLastAttack = attackTimeout;
    }

    bool CanAttack() => timeSinceLastAttack >= attackTimeout && !IsAttackBlocked;

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
