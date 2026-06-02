using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionComponent : MonoBehaviour
{
    [field:SerializeField] public float Distance { get; private set; }
    [SerializeField] TextMeshProUGUI interactionText;

    PlayerController playerController;

    readonly List<GameObject> interactables = new();

    public void InteractWithCurrent()
    {
        if (TryGetClosestInteractable(out IPlayerInteractable interactable))
        {
            interactable.Interact(playerController);
        }
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void SetInteractionText(string text)
    {
        if (!interactionText)
            return;

        if (text.Length == 0)
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
        if (collision.TryGetComponent(out IPlayerInteractable _))
        {
            interactables.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactables.Contains(collision.gameObject))
        {
            interactables.Remove(collision.gameObject);
        }
    }

    bool TryGetClosestInteractable(out IPlayerInteractable interactable)
    {
        interactable = null;
        GameObject gameObject = interactables
            .OrderBy(i => Physics2D.Distance(i.GetComponent<Collider2D>(), playerController.collider2D).distance)
             .FirstOrDefault();

        if (!gameObject)
            return false;

        interactable = gameObject.GetComponent<IPlayerInteractable>();
        return interactable != null;
    }

    private void Update()
    {
        if (TryGetClosestInteractable(out IPlayerInteractable interactable))
        {
            SetInteractionText(interactable.InteractionText);
        }
    }
}
