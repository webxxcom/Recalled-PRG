using UnityEngine;

public class Chest : InteractableObject
{
    [field: SerializeField] public ItemDefinition RequiredKey { get; private set; }

    [SerializeField] LootTable _lootTable;
    [SerializeField] InventorySO _inventory;

    public override void Interact()
    {
        if (!IsInteracted && PlayerCanInteract())
            Open();
    }

    void Open()
    {
        IsInteracted = true;
        _inventory.Remove(RequiredKey);
        _inventory.Add(_lootTable.GetItem());
    }

    protected override bool PlayerCanInteract()
    {
        return RequiredKey == null || _inventory.Contains(RequiredKey);
    }
}
