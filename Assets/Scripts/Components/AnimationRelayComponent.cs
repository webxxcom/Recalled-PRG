using UnityEngine;

public class AnimationRealyComponent : MonoBehaviour
{
    [SerializeField] EnemyAttackComponent enemyAttackComponent;

    public void ExecuteAttack()
    {
        enemyAttackComponent.ExecuteAttack();
    }
}
