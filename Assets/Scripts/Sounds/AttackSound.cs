using UnityEngine;

[RequireComponent(typeof(EntityAudioController))]
public class AttackSound : EntitySoundComponent
{
    [SerializeField] AudioClip _attackSound;
    [SerializeField] DefaultAttackComponent entityAttackComponent;

    EntityAudioController entityAudioController;

    private void Awake()
    {
        entityAudioController = GetComponent<EntityAudioController>();
    }

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
         entityAudioController.AudioSource.PlayOneShot(_attackSound);
    }
}
