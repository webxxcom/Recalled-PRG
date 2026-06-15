using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EntityMovementSound : EntitySoundComponent
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] float _delayBetween;

    AudioSource _audioSource;
    MovementBase _movementBase;
    bool _isPlaying;

    protected override void Awake()
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();
        _movementBase = GetComponentInParent<MovementBase>();
    }

    public override void Activate()
    {
        _movementBase.OnMovementStarted += StartPlaying;
        _movementBase.OnMovementStopped += StopPlaying;
    }

    public override void Deactivate()
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
            AudioSource.PlayOneShot(_walkingSound);
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
