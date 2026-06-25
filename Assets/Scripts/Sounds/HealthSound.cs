using UnityEngine;

public class HealthSound : EntitySoundComponent
{
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healingSound;
    [SerializeField] AudioClip _deathSound;

    HealthComponent healthComponent;

    public override void Activate()
    {
        if(!healthComponent)
            healthComponent = GetComponentInParent<HealthComponent>();

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
        AudioSource.PlayOneShot(_deathSound);
    }

    void HandleHurtHealingSound(GameObject _, int val)
    {
        if (val < 0)
            AudioSource.PlayOneShot(_hurtSound);
        else if (val > 0)
            AudioSource.PlayOneShot(_healingSound);
    }
}
