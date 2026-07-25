using UnityEngine;

public class HealthSound : EntitySoundComponent
{
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healingSound;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] HealthProvider _healthProvider;

    void OnEnable()
    {
        _healthProvider.Value.OnMinValueReached += HandleDeathSound;
        _healthProvider.Value.OnValueChanged += HandleHurtHealingSound;
    }

    void OnDisable()
    {
        _healthProvider.Value.OnMinValueReached += HandleDeathSound;
        _healthProvider.Value.OnValueChanged += HandleHurtHealingSound;
    }

    public void HandleDeathSound(GameObject _)
    {
        _audioSource.PlayOneShot(_deathSound);
    }

    public void HandleHurtHealingSound(GameObject _, int val)
    {
        if (val < 0)
            _audioSource.PlayOneShot(_hurtSound);
        else if (val > 0)
            _audioSource.PlayOneShot(_healingSound);
    }
}
