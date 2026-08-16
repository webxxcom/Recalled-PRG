using UnityEngine;

public class Chest : InteractableObject
{
    [SerializeField] ItemDefinition _requiredKey;
    [SerializeField] LootTable _lootTable;
    [SerializeField] InventorySO _inventory;

    public override void Interact()
    {
        if (PlayerCanInteract())
            Open();
    }

    void Open()
    {
        IsInteracted = true;
        _inventory.Remove(_requiredKey);
        _inventory.Add(_lootTable.GetItem());
    }

    public override bool PlayerCanInteract()
    {
        return (_requiredKey == null || _inventory.Contains(_requiredKey)) && !IsInteracted;
    }
}
