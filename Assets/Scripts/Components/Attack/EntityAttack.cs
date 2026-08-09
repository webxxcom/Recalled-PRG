using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public abstract class EntityAttack : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected List<AttackStrategy> _attackStrategies = new();

    [SerializeField] protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;
    public event Action OnAttackFinished;

    protected void Attack(AttackStrategy attackStrategy)
    {
        _currentAttack = attackStrategy;

        _animator.SetTrigger(attackStrategy.AnimatorHash);
    }

    AttackStrategy _currentAttack;
    public void StartAttack()
    {
        _currentAttack.StartExecuting();

        OnAttackStarted?.Invoke();
    }

    public void ProcessAttack(float normalizedTime)
    {
        _currentAttack.ProcessState(normalizedTime);
    }

    public void FinishAttack()
    {
        _currentAttack.FinishExecuting();

        OnAttackFinished?.Invoke();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_attackStrategies.Count == 0)
             GetComponents(_attackStrategies);
    }

#endif
}
