using UnityEngine;

public class ChestScript : InteractableObjectScript
{
    [field: SerializeField] public InventoryItem RequiredKey { get; private set; }

    public override void Interact()
    {
        if (!IsInteracted && PlayerCanInteract())
            Open();
    }

    void Open()
    {
        IsInteracted = true;
    }

    protected override bool PlayerCanInteract()
    {
        return FindAnyObjectByType<PlayerController>().Inventory.Contains(RequiredKey);
    }
}
