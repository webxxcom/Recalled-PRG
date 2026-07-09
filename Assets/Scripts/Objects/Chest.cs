using UnityEngine;

public class Chest : InteractableObject
{
    [field: SerializeField] public ItemDefinition RequiredKey { get; private set; }

    PlayerInventory _playerInventory;

    protected override void Awake()
    {
        base.Awake();

        _playerInventory = Utils.FindOrThrow(FindAnyObjectByType<PlayerInventory>);
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
