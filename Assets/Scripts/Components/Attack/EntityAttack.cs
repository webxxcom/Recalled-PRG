using System;
using UnityEngine;

[RequireComponent(typeof(AttackStrategy))]
public abstract class EntityAttack : MonoBehaviour
{
    [SerializeField] Animator _animator;
    
    protected AttackStrategy _attackStrategy;
    protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;
    public event Action OnAttackFinished;

    protected virtual void Awake()
    {
        _attackStrategy = GetComponent<AttackStrategy>();
    }

    private void Start()
    {
        _timeSinceLastAttack = _attackStrategy.AttackData.ReloadTime;
    }

    protected void Attack()
    {
        _animator.SetTrigger(AnimatorParameters.AttackHash);
    }

    public void StartAttack()
    {
        _attackStrategy.StartExecuting();

        OnAttackStarted?.Invoke();
    }

    public void ProcessAttack(float normalizedTime)
    {
        _attackStrategy.ProcessState(normalizedTime);
    }

    public void FinishAttack()
    {
        _attackStrategy.FinishExecuting();
        OnAttackFinished?.Invoke();
    }
}
