using UnityEngine;

public interface IPlayerInteractable
{
    public string InteractionText { get; }

    public void Interact(PlayerController interacter);
}
