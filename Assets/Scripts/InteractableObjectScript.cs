using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class InteractableObjectScript : MonoBehaviour
{
    private static readonly int InteractHash = Animator.StringToHash("Interact");

    public GameObject InteractionText { get; protected set; }

    public bool IsInteracted
    {
        get => _IsInteracted;
        protected set
        {
            if (value)
            {
                animator.SetTrigger(InteractHash);
                InteractionText.SetActive(false);
            }
            _IsInteracted = value;
        }
    }

    bool _IsInteracted;
    Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        InteractionText = GetComponentInChildren<TextMeshProUGUI>().gameObject;
    }

    private void Start()
    {
        InteractionText.SetActive(false);
    }

    // Method used in the trigger to decide if at the current moment player can interact with the object
    // whether it's an availability of a key in player's inventory to open a chest or a specific looking into the picture
    protected abstract bool PlayerCanInteract(PlayerController playerController);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInteracted && collision.TryGetComponent(out PlayerInteractionComponent _)
            && PlayerCanInteract(collision.GetComponentInParent<PlayerController>()))
        {
            InteractionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsInteracted && collision.TryGetComponent(out PlayerInteractionComponent _))
        {
            InteractionText.SetActive(false);
        }
    }

    public abstract void Interact();
}
