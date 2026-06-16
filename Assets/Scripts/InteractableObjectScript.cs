using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class InteractableObjectScript : MonoBehaviour
{
    private static readonly int InteractHash = Animator.StringToHash("Interact");

    [field: SerializeField] public AudioClip FirstStateAudio { get; private set; }
    [field: SerializeField] public AudioClip SecondStateAudio { get; private set; }

    public GameObject InteractionText { get; protected set; }

    public bool IsInteracted
    {
        get => _IsInteracted;
        protected set
        {
            if (value)
            {
                animator.SetTrigger(InteractHash);
                audioSource.PlayOneShot(FirstStateAudio);
                InteractionText.SetActive(false);
            }
            else
            {
                audioSource.PlayOneShot(SecondStateAudio);
            }
            _IsInteracted = value;
        }
    }

    bool _IsInteracted;
    Animator animator;
    AudioSource audioSource;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        InteractionText = GetComponentInChildren<TextMeshProUGUI>().gameObject;
    }

    private void Start()
    {
        InteractionText.SetActive(false);
    }

    // Method used in the trigger to decide if at the current moment player can interact with the object
    // whether it's an availability of a key in player's inventory to open a chest or a specific looking into the picture
    protected abstract bool PlayerCanInteract();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInteracted && collision.TryGetComponent(out PlayerInteractionComponent _)
            && PlayerCanInteract())
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
