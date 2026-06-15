using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MovementSound : EntitySoundComponent
{
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] float _deltaTime;

    PlayerMovementComponent playerMovementComponent;

    protected override void Awake()
    {
        base.Awake();

        playerMovementComponent = GetComponentInParent<PlayerMovementComponent>();
    }

    public override void Activate()
    {
        playerMovementComponent.OnMovement += HandleMovementSound;
    }

    private void OnDisable()
    {
        Deactivate();
    }

    public override void Deactivate()
    {
        playerMovementComponent.OnMovement -= HandleMovementSound;
    }

    float timeSince = 0;
    void HandleMovementSound()
    {
        timeSince += Time.deltaTime;

        if (timeSince > _deltaTime && playerMovementComponent.IsWalking)
        {
            AudioSource.PlayOneShot(_walkingSound);
            timeSince = 0;
        }
    }

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
    private void Update()
    {
        timeSince += Time.deltaTime;

        if (timeSince > DelayBeetweenPlays() && playerMovementComponent.IsWalking)
        {
            AudioSource.PlayOneShot(_walkingSound);
            timeSince = 0;
        }
    }
}
