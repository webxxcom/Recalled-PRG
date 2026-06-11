using UnityEngine;

public class ChestScript : InteractableObjectScript
{
    [field: SerializeField] public KeyDefinition RequiredKey { get; private set; }

    public override void Interact(PlayerController interacter)
    {
       
    }

    void OpenChest(PlayerController interacter)
    {
        IsInteracted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteractionComponent _))
        {
            InteractionText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteractionComponent _))
        {
            InteractionText.SetActive(false);
        }
    }
}
