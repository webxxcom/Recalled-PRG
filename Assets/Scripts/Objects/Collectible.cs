using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
    private static readonly int CollectedHash = Animator.StringToHash("Collected");

    [field: SerializeField] public ItemDefinition InventoryItemDefinition { get; private set; }
    [field: SerializeField] public int Quantity { get; private set; }
    [field: SerializeField] public AudioClip PickUpSound { get; private set; }

    public bool IsCollected { get; private set; }
    Animator animator;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCollected)
            return;

        if (collision.TryGetComponent(out PlayerController pc))
        {
            pc.Inventory.Add(InventoryItemDefinition, Quantity);

            IsCollected = true;
            if (animator)
                animator.SetTrigger(CollectedHash);
        }
    }

    static public Collectible Instantiate(ItemDefinition inventoryItem, int quantity)
    {
        return new()
        {
            InventoryItemDefinition = inventoryItem,
            Quantity = quantity
        };
    }
}
