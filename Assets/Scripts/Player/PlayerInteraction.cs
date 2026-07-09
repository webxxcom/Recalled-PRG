using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;

    PlayerController _playerController;
    readonly List<GameObject> _interactables = new();

    public void InteractWithCurrent()
    {
        if (TryGetClosestInteractable(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }

    private void Awake()
    {
        _playerController = Utils.FindOrThrow(GetComponentInParent<PlayerController>);
    }

    void SetInteractionText(string text)
    {
        if (!interactionText)
            return;

        if (text == null || text.Length == 0)
        {
            interactionText.enabled = false;
        }
        else
        {
            interactionText.enabled = true;
            interactionText.SetText(text);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable _))
        {
            _interactables.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactables.Contains(collision.gameObject))
        {
            _interactables.Remove(collision.gameObject);
        }
    }

    bool TryGetClosestInteractable(out IInteractable interactable)
    {
        interactable = null;
        GameObject gameObject = _interactables
            .OrderBy(i => Physics2D.Distance(i.GetComponent<Collider2D>(), _playerController.Collider2D).distance)
             .FirstOrDefault();

        if (!gameObject)
            return false;

        interactable = gameObject.GetComponent<IInteractable>();
        return interactable != null;
    }
}
