using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class AnimatorRelay : MonoBehaviour
{
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");

    [SerializeField] HealthProvider _health;
    [SerializeField] Animator _animator;
    [SerializeField] MovementBase _movementBase;
    EntityController _entityController;

    private void Awake()
    {
        _entityController = GetComponent<EntityController>();
    }

    private void OnEnable()
    {
        _health.OnValueChanged += HpChanged;
        _health.OnMinValue += OnDeath;
    }

    private void OnDisable()
    {
        _health.OnValueChanged -= HpChanged;
        _health.OnMinValue -= OnDeath;
    }

    void HpChanged(DamageInfo damageInfo)
    {
        _animator.SetTrigger(HurtHash);
        
        damageInfo.Effects?.ForEach(e => _health.EffectMachine.ApplyEffect(_entityController, _health, e));
    }

    void OnDeath(DamageInfo damageInfo)
    {
        _animator.SetTrigger(DieHash);
        _movementBase.enabled = false;

        Debug.Log($"{gameObject.name} was killed by {damageInfo.Source.name}");
    }
}
