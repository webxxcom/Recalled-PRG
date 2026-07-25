using UnityEngine;

public class PlayerMovementSound : EntitySoundComponent
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] PlayerMovement _playerMovementComponent;

    bool _isPlaying;

    void OnEnable()
    {
        _playerMovementComponent.OnMovementStarted += StartPlaying;
        _playerMovementComponent.OnMovementStopped += StopPlaying;
    }

    void OnDisable()
    {
        _playerMovementComponent.OnMovementStarted -= StartPlaying;
        _playerMovementComponent.OnMovementStopped -= StopPlaying;
    }

    void StartPlaying() => _isPlaying = true;
    void StopPlaying() => _isPlaying = false;

    float DelayBeetweenPlays()
    {
        float kf = _playerMovementComponent.SpeedAggregator.Get();

        if (_playerMovementComponent.IsSprinting)
        {
            return 0.4f / kf * _playerMovementComponent.SprintingSpeedMultiplier;
        }
        else
            return 0.3f / kf;
    }

    float timeSince = 0;
    void UpdateMovementSound()
    {
        if (timeSince > DelayBeetweenPlays() && _playerMovementComponent.IsWalking)
        {
            _audioSource.PlayOneShot(_walkingSound);
            timeSince = 0;
        }
    }

    private void Update()
    {
        timeSince += Time.deltaTime;

        if (_isPlaying)
            UpdateMovementSound();
    }
}
