using UnityEngine;

public class HealthSound : EntitySoundComponent
{
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healingSound;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] HealthProvider _healthProvider;

    public override void Activate()
    {
        _healthProvider.Health.OnMinValueReached += HandleDeathSound;
        _healthProvider.Health.OnValueChanged += HandleHurtHealingSound;
    }

    public override void Deactivate()
    {
        _healthProvider.Health.OnMinValueReached += HandleDeathSound;
        _healthProvider.Health.OnValueChanged += HandleHurtHealingSound;
    }

    public void HandleDeathSound(GameObject _)
    {
        AudioSource.PlayOneShot(_deathSound);
    }

    public void HandleHurtHealingSound(GameObject _, int val)
    {
        if (val < 0)
            AudioSource.PlayOneShot(_hurtSound);
        else if (val > 0)
            AudioSource.PlayOneShot(_healingSound);
    }
}
