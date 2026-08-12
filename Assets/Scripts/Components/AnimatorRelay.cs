using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class AnimatorRelay : MonoBehaviour
{
    [SerializeField] HealthResource _health;
    [SerializeField] Animator _animator;
    [SerializeField] MovementBase _movementBase;
    [SerializeField] EntityAttack _entityAttack;
    [SerializeField] EntityController _entityController;

    private void OnEnable()
    {
        _health.OnHpChangeApplied += HpChanged;
        _health.OnDeath += OnDeath;
        _entityAttack.OnAttackStarted += AttackStart;
        _entityAttack.OnAttackFinished += AttackFinish;
    }

    private void OnDisable()
    {
        _health.OnHpChangeApplied -= HpChanged;
        _health.OnDeath -= OnDeath;
        _entityAttack.OnAttackStarted -= AttackStart;
        _entityAttack.OnAttackFinished -= AttackFinish;
    }

    void AttackStart()
    {
        //_movementBase.SpeedAggregator.Add(_entityAttack.AttackData.SpeedMultiplier);
    }

    void AttackFinish()
    {
        //_movementBase.SpeedAggregator.Remove(_entityAttack.AttackData.SpeedMultiplier);
    }

    void HpChanged(DamageInfo damageInfo)
    {
        _animator.SetTrigger(AnimatorParameters.HurtHash);
        
        damageInfo.Effects?.ForEach(e => _health.EffectMachine.ApplyEffect(_entityController, _health, e));
    }

    void OnDeath(DamageInfo damageInfo)
    {
        _animator.SetTrigger(AnimatorParameters.DieHash);
        _movementBase.enabled = false;

        Debug.Log($"{gameObject.name} was killed by {damageInfo.Source.name}");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_entityAttack == null)
            _entityAttack = GetComponentInChildren<EntityAttack>();
        if (_movementBase == null)
            _movementBase = GetComponentInChildren<MovementBase>();
        if (_entityController == null)
            _entityController = GetComponent<EntityController>();
        if (_health == null)
            _health = GetComponentInChildren<HealthResource>();
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }
#endif
}
