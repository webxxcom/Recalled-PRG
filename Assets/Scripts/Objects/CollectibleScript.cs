using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class CollectibleScript : MonoBehaviour
{
    [field: SerializeField] public InventoryItem InventoryItemDefinition { get; private set; }
    [field: SerializeField] public int Quantity { get; private set; }

    public bool IsCollected { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCollected)
            return;

        if (collision.TryGetComponent(out PlayerController pc))
        {
            pc.Inventoty.Add(InventoryItemDefinition, Quantity);

            IsCollected = true;
            GetComponent<Animator>().SetTrigger("Collected");
        }
    }
}
