using UnityEngine;

public class ChestScript : InteractableObjectScript
{
    [field: SerializeField] public InventoryItem RequiredKey { get; private set; }

    public override void Interact() => Open();

    void Open()
    {
        IsInteracted = true;
    }

    protected override bool PlayerCanInteract(PlayerController playerController)
    {
        return playerController && playerController.Inventory.Contains(RequiredKey);
    }
}
