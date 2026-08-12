using UnityEngine;

public class EntityMovementSound : EntitySound
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] float _delayBetween;
    [SerializeField] MovementBase _movementBase;
    [SerializeField] SpeedAggregator _speedAggregator;

    bool _isPlaying;

    void OnEnable()
    {
        _movementBase.OnMovementStarted += StartPlaying;
        _movementBase.OnMovementStopped += StopPlaying;
    }

    void OnDisable()
    {
        _movementBase.OnMovementStarted -= StartPlaying;
        _movementBase.OnMovementStopped -= StopPlaying;
    }

    void StartPlaying() => _isPlaying = true;
    void StopPlaying() => _isPlaying = false;

    float _elapsed = 0;
    void UpdateMovementSound()
    {
        if (_elapsed > _delayBetween / (_speedAggregator != null ? _speedAggregator.Get() : 1f) && _movementBase.IsWalking)
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
}
