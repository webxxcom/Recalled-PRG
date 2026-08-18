using UnityEngine;

public class PlayerMovementSound : EntitySound
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] PlayerMovement _playerMovement;

    bool _isPlaying;

    void OnEnable()
    {
        _playerMovement.OnMovementStarted += StartPlaying;
        _playerMovement.OnMovementStopped += StopPlaying;
    }

    void OnDisable()
    {
        _playerMovement.OnMovementStarted -= StartPlaying;
        _playerMovement.OnMovementStopped -= StopPlaying;
    }

    void StartPlaying() => _isPlaying = true;
    void StopPlaying() => _isPlaying = false;

    float _elapsed = 0;
    void UpdateMovementSound()
    {
        float DelayBeetweenPlays = (_playerMovement.IsSprinting ? 0.4f : 0.3f) / _playerMovement.CurrentSpeed;
        _elapsed += Time.deltaTime;

        if (_elapsed > DelayBeetweenPlays && _playerMovement.IsWalking)
        {
            _audioSource.PlayOneShot(_walkingSound);

            _elapsed = 0;
        }
    }

    private void Update()
    {
        if (_isPlaying)
            UpdateMovementSound();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_playerMovement == null)
            _playerMovement = GetComponentInParent<PlayerMovement>();
    }
#endif
}
