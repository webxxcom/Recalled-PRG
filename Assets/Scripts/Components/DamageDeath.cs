using UnityEngine;

public class DamageDeath : HealthReactor
{
    [SerializeField] Animator _animator;

    protected override void OnDeath(DamageInfo di)
    {
        _animator.SetTrigger(AnimatorParameters.DieHash);
    }
}
