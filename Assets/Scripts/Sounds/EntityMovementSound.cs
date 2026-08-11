using UnityEngine;

public class EntityMovementSound : EntitySound
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] float _delayBetween;
    [SerializeField] MovementBase _movementBase;

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

    float timeSince = 0;
    void UpdateMovementSound()
    {
        if (timeSince > _delayBetween / _movementBase.SpeedAggregator.Get() && _movementBase.IsWalking)
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
