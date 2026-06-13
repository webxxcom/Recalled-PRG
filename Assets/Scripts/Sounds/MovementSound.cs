using UnityEngine;

[RequireComponent(typeof(EntityAudioController))]
public class MovementSound : EntitySoundComponent
{
    [SerializeField] AudioClip _walkingSound;

    EntityAudioController entityAudioController;
    MovementBase movementBase;

    private void Awake()
    {
        entityAudioController = GetComponent<EntityAudioController>();
        movementBase = GetComponentInParent<MovementBase>();
    }

    public override void Activate()
    {
        movementBase.OnMovement += HandleMovementSound;
    }

    private void OnDisable()
    {
        Deactivate();
    }

    public override void Deactivate()
    {
        movementBase.OnMovement -= HandleMovementSound;
    }

    void HandleMovementSound()
    {
        entityAudioController.AudioSource.PlayOneShot(_walkingSound);
    }
}
