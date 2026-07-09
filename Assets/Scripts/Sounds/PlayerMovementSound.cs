using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovementSound : EntitySoundComponent
{
    [SerializeField] AudioClip _walkingSound;

    PlayerMovement playerMovementComponent;
    bool _isPlaying;

    protected override void Awake()
    {
        base.Awake();

        playerMovementComponent = GetComponentInParent<PlayerMovement>();
    }

    public override void Activate()
    {
        playerMovementComponent.OnMovementStarted += StartPlaying;
        playerMovementComponent.OnMovementStopped += StopPlaying;
    }

    public override void Deactivate()
    {
        playerMovementComponent.OnMovementStarted -= StartPlaying;
        playerMovementComponent.OnMovementStopped -= StopPlaying;
    }

    void StartPlaying() => _isPlaying = true;
    void StopPlaying() => _isPlaying = false;

    float DelayBeetweenPlays()
    {
        float kf = playerMovementComponent.SpeedAggregator.Get();

        if (playerMovementComponent.IsSprinting)
        {
            return 0.4f / kf * playerMovementComponent.SprintingSpeedMultiplier;
        }
        else
            return 0.3f / kf;
    }
    float timeSince = 0;
    void UpdateMovementSound()
    {
        if (timeSince > DelayBeetweenPlays() && playerMovementComponent.IsWalking)
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
