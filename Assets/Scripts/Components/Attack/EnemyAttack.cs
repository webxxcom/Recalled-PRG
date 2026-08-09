using UnityEngine;

public class EnemyAttack : EntityAttack
{
    [SerializeField] float _intervalBetweenStates;

    void Update()
    {
        if (_timeSinceLastAttack > _intervalBetweenStates)
        {
            foreach (var attackStrategy in _attackStrategies)
            {
                if (attackStrategy.Ready)
                {
                    Attack(attackStrategy);
                    _timeSinceLastAttack = 0;
                    break;
                }
            }
        }
        _timeSinceLastAttack += Time.deltaTime;
    }
}
