using UnityEngine;

class ProjectileAttackStrategy : AttackStrategy
{
    [field: SerializeField] public ProjectileAttackDataSO ProjectileAttackData { get; private set; }
    [SerializeField] Animator _animator;
    public override AttackSO AttackData => ProjectileAttackData;

    protected override bool WithinAttackRange { get; set; } = true;

    public override int AnimatorHash => AnimatorParameters.ShootHash;

    public override void FinishExecuting()
    {
    }

    bool _completed;
    public override void ProcessState(float normalizedTime)
    {
        if (normalizedTime >= ProjectileAttackData.NormalizedSpawnPoint && !_completed)
        {
            GameObject projectile = Instantiate(ProjectileAttackData.ProjectilePrefab, _animator.transform.position, Quaternion.identity);

            projectile.GetComponent<ProjectileScript>().Initialize(transform.parent.gameObject, FindAnyObjectByType<PlayerController>().transform.position);
            _completed = true;
        }
    }

    public override void StartExecuting()
    {
        _completed = false;
        _elapsedSinceAttack = 0;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_animator == null)
            _animator = transform.root.GetComponentInChildren<Animator>();
    }
#endif
}
