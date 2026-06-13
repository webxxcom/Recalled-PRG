using UnityEngine;

[RequireComponent(typeof(EntityAudioController))]
public class HealthSound : EntitySoundComponent
{
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healingSound;
    [SerializeField] AudioClip _deathSound;

    EntityAudioController entityAudioController;
    HealthComponent healthComponent;

    private void Awake()
    {
        entityAudioController = GetComponent<EntityAudioController>();
        healthComponent = GetComponentInParent<HealthComponent>();
    }

    public override void Activate()
    {
        healthComponent.OnMinValueReached += HandleDeathSound;
        healthComponent.OnValueChanged += HandleHurtHealingSound;
    }

    public override void Deactivate()
    {
        healthComponent.OnMinValueReached -= HandleDeathSound;
        healthComponent.OnValueChanged -= HandleHurtHealingSound;
    }

    void HandleDeathSound(GameObject _)
    {
        entityAudioController.AudioSource.PlayOneShot(_deathSound);
    }

    void HandleHurtHealingSound(GameObject _, int val)
    {
        if (val < 0)
            entityAudioController.AudioSource.PlayOneShot(_hurtSound);
        else if (val > 0)
            entityAudioController.AudioSource.PlayOneShot(_healingSound);
    }
}
