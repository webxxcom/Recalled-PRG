using UnityEngine;

public class PlayerMovementSound : EntitySound
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] PlayerMovement _playerMovementComponent;
    [SerializeField] SpeedAggregator _speedAggregator;
    [SerializeField] PlayerSprinting _playerSprinting;

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
        float kf = _speedAggregator.Get();

        if (_playerSprinting.IsActive)
        {
            return 0.4f / kf * _playerSprinting.SpeedMultiplier;
        }
        else
            return 0.3f / kf;
    }

    float _elapsed = 0;
    void UpdateMovementSound()
    {
        if (_elapsed > DelayBeetweenPlays() && _playerMovementComponent.IsWalking)
        {
            _audioSource.PlayOneShot(_walkingSound);
            _elapsed = 0;
        }
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;

        if (_isPlaying)
            UpdateMovementSound();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_speedAggregator == null)
            _speedAggregator = GetComponentInParent<SpeedAggregator>();
    }
#endif
}
