using UnityEngine;

public class HealthSound : EntitySoundComponent
{
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healingSound;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] HealthProvider _healthProvider;

    void OnEnable()
    {
        _healthProvider.OnMinHpReached += HandleDeathSound;
        _healthProvider.OnHpChanged += HandleHurtHealingSound;
    }

    void OnDisable()
    {
        _healthProvider.OnMinHpReached += HandleDeathSound;
        _healthProvider.OnHpChanged += HandleHurtHealingSound;
    }

    public void HandleDeathSound(DamageInfo damageInfo)
    {
        _audioSource.PlayOneShot(_deathSound);
    }

    public void HandleHurtHealingSound(DamageInfo damageInfo)
    {
        if (damageInfo.Amount < 0)
            _audioSource.PlayOneShot(_hurtSound);
        else if (damageInfo.Amount > 0)
            _audioSource.PlayOneShot(_healingSound);
    }
}
