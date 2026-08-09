using UnityEngine;

[RequireComponent(typeof(EntityAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    public abstract int AnimatorHash { get; }
    public abstract AttackSO AttackData { get; }
    protected abstract bool WithinAttackRange { get; set; }
    public bool Ready => AttackData.ReloadTime <= _elapsedSinceAttack && WithinAttackRange;

    protected float _elapsedSinceAttack;

    public abstract void StartExecuting();
    public abstract void ProcessState(float normalizedTime);
    public abstract void FinishExecuting();

    private void Update()
    {
        _elapsedSinceAttack += Time.deltaTime;
    }
}
