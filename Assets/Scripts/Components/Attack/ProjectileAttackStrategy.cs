using UnityEngine;

class ProjectileAttackStrategy : AttackStrategy
{
    [SerializeField] ProjectileAttackDataSO _projectileAttackData;
    public override AttackSO AttackData => _projectileAttackData;
    protected override bool WithinAttackRange { get; set; } = true;
    public override int AnimatorHash => AnimatorParameters.ShootHash;

    [SerializeField] AnimationController _animationController;

    bool _completed;
    public override void ProcessState(float normalizedTime)
    {
        if (normalizedTime >= _projectileAttackData.NormalizedSpawnPoint && !_completed)
        {
            GameObject projectile = Instantiate(_projectileAttackData.ProjectilePrefab, transform.position, Quaternion.identity);

            // TODO trashy init
            projectile.GetComponent<ProjectileScript>().Initialize(
                transform.parent.gameObject,
                FindAnyObjectByType<PlayerController>().transform.position,
                _animationController.FlippedX);

            _completed = true;
        }
    }

    public override void StartExecuting()
    {
        _completed = false;
        _elapsedSinceAttack = 0;
    }

    public override void FinishExecuting()
    {
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_animationController == null)
            _animationController = transform.parent.GetComponentInChildren<AnimationController>();
    }
#endif
}
