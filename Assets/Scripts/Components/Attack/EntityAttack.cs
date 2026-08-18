using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public abstract class EntityAttack : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected List<AttackStrategy> _attackStrategies = new();

    protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;
    public event Action OnAttackFinished;

    protected AttackStrategy _currentAttack;
    protected AttackContext _attackContext;
    protected void Attack(AttackStrategy attackStrategy, AttackContext attackContext)
    {
        _currentAttack = attackStrategy;
        _attackContext = attackContext;

        _animator.SetTrigger(attackStrategy.AnimatorHash);
    }

    public void StartAttack()
    {
        _currentAttack.StartExecuting(_attackContext);

        OnAttackStarted?.Invoke();
    }

    public void ProcessAttack(float normalizedTime)
    {
        _currentAttack.ProcessState(normalizedTime, _attackContext);
    }

    public void FinishAttack()
    {
        _currentAttack.FinishExecuting(_attackContext);

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
