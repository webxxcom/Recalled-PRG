using UnityEngine;

public class ChestScript : InteractableObjectScript
{
    [field: SerializeField] public InventoryItem RequiredKey { get; private set; }

    PlayerInventoryComponent _playerInventory;

    protected override void Awake()
    {
        base.Awake();

        _playerInventory = Utils.FindOrThrow(FindAnyObjectByType<PlayerInventoryComponent>);
    }

    public override void Interact()
    {
        if (!IsInteracted && PlayerCanInteract())
            Open();
    }

    void Open()
    {
        IsInteracted = true;
        _playerInventory.Remove(RequiredKey);
    }

    protected override bool PlayerCanInteract()
    {
        return FindAnyObjectByType<PlayerController>().Inventory.Contains(RequiredKey);
    }
}
