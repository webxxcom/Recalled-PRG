using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteraction : MonoBehaviour
{
    readonly List<IInteractable> _interactables = new();

    public void InteractWithCurrent() => _interactables.FirstOrDefault()?.Interact();

    void OnInteract(InputValue _) => InteractWithCurrent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            _interactables.Add(interactable);

            if (interactable is InteractableObject interactableObject)
                interactableObject.ShowInteractionText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable)
                && _interactables.Contains(interactable))
        {
            _interactables.Remove(interactable);

            if (interactable is InteractableObject interactableObject)
                interactableObject.HideInteractionText();
        }
    }
}
