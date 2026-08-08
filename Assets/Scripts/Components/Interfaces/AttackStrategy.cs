using UnityEngine;

[RequireComponent(typeof(EntityAttack))]
public abstract class AttackStrategy : MonoBehaviour
{
    public abstract AttackSO AttackData { get; }

    public abstract void StartExecuting();
    public abstract void ProcessState(float normalizedTime);
    public abstract void FinishExecuting();
}
