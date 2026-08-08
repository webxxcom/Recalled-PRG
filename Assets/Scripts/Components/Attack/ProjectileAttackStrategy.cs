using UnityEngine;

class ProjectileAttackStrategy : AttackStrategy
{
    [field: SerializeField] public ProjectileAttackDataSO ProjectileAttackData { get; private set; }
    [field: SerializeField] Animator _animator;
    public override AttackSO AttackData => ProjectileAttackData;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_animator == null)
            _animator = transform.root.GetComponentInChildren<Animator>();
    }
#endif

    public override void FinishExecuting()
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessState(float normalizedTime)
    {
        if (normalizedTime >= ProjectileAttackData.NormalizedSpawnPoint)
        {
            GameObject projectile = Instantiate(ProjectileAttackData.ProjectilePrefab, _animator.transform.position, Quaternion.identity);

            projectile.GetComponent<ProjectileScript>().Initialize(name, Vector2.right);
        }
    }

    public override void StartExecuting()
    {
        throw new System.NotImplementedException();
    }
}
