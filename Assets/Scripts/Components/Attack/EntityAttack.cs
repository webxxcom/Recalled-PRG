using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class EntityAttack : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [field: SerializeField] public AttackSO AttackData { get; private set; }
    [SerializeField] Animator _animator;

    public CapsuleCollider2D Hitbox { get; private set; }

    protected float _timeSinceLastAttack;

    public event Action OnAttackStarted;

    protected virtual void Awake()
    {
        Hitbox = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        _timeSinceLastAttack = AttackData.ReloadTime;
    }

    protected void Attack()
    {
        _animator.SetTrigger(AttackHash);

        OnAttackStarted?.Invoke();
    }
}
