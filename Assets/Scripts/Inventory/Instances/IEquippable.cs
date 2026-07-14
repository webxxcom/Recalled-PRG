internal interface IEquippable
{
    ItemInstance Equip(PlayerInventory playerInventory)
    {
        ItemInstance toSet = this as ItemInstance;
        ItemInstance replaced = GetInTheInventory(playerInventory);

        playerInventory.Add(replaced);
        SetInTheInventory(playerInventory, toSet);
        playerInventory.Remove(toSet);

        return replaced;
    }
    ItemInstance Unequip(PlayerInventory playerInventory)
    {
        ItemInstance cpy = GetInTheInventory(playerInventory);

        playerInventory.Add(cpy);
        SetInTheInventory(playerInventory, null);
        return cpy;
    }

    protected ItemInstance GetInTheInventory(PlayerInventory playerInventory);
    protected void SetInTheInventory(PlayerInventory playerInventory, ItemInstance val);
}
