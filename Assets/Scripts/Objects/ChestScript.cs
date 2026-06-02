using UnityEngine;

public class ChestScript : InteractableObjectScript, IPlayerInteractable
{
    [field: SerializeField] public KeyDefinition RequiredKey { get; private set; }

    public string InteractionText => "Press E to open chest";

    public void Interact(PlayerController interacter)
    {
        if (interacter.inventory.Contains(RequiredKey))
            OpenChest(interacter);
    }

    void OpenChest(PlayerController interacter)
    {
        IsInteracted = true;
        interacter.ChestsUnlocked++;
        interacter.RemoveKey(RequiredKey);
    }
}
