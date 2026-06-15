using UnityEngine;

public class AttackSound : EntitySoundComponent
{
    [SerializeField] AudioClip _attackSound;
    [SerializeField] DefaultAttackComponent entityAttackComponent;

    public override void Activate()
    {
        entityAttackComponent.OnAttackEvent += HandleAttackSound;
    }

    public override void Deactivate()
    {
        entityAttackComponent.OnAttackEvent -= HandleAttackSound;
    }

    void HandleAttackSound()
    {
         AudioSource.PlayOneShot(_attackSound);
    }
}
