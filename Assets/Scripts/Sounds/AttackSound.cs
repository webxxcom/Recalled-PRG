using UnityEngine;

public class AttackSound : EntitySoundComponent
{
    [SerializeField] AudioClip _attackSound;
    [SerializeField] EntityAttack entityAttackComponent;

    public override void Activate()
    {
        entityAttackComponent.OnAttackStarted += HandleAttackSound;
    }

    public override void Deactivate()
    {
        entityAttackComponent.OnAttackStarted -= HandleAttackSound;
    }

    void HandleAttackSound()
    {
         AudioSource.PlayOneShot(_attackSound);
    }
}
