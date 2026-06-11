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
            animator.SetTrigger(InteractHash);
            _IsInteracted = value;
        }
    }

    bool _IsInteracted;
    Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        InteractionText = GetComponentInChildren<TextMeshProUGUI>().gameObject;
    }

    private void Start()
    {
        InteractionText.SetActive(false);
    }

    public abstract void Interact(PlayerController interacter);
}
