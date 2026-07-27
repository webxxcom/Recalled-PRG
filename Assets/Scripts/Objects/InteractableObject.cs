using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private static readonly int InteractHash = Animator.StringToHash("Interact");

    [SerializeField] AudioClip _firstStateAudio;
    [SerializeField] AudioClip _secondStateAudio;
    [SerializeField] string _displayText;
    [SerializeField] TextMeshProUGUI _interactionTextMesh;

    public bool IsInteracted
    {
        get => _IsInteracted;
        protected set
        {
            if (value)
            {
                _animator.SetTrigger(InteractHash);
                _audioSource.PlayOneShot(_firstStateAudio);
                _interactionTextMesh.gameObject.SetActive(false);
            }
            else
            {
                _audioSource.PlayOneShot(_secondStateAudio);
            }
            _IsInteracted = value;
        }
    }

    bool _IsInteracted;
    Animator _animator;
    AudioSource _audioSource;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _interactionTextMesh.text = _displayText;
        _interactionTextMesh.gameObject.SetActive(false);
    }

    // Method used in the trigger to decide if at the current moment player can interact with the object
    // whether it's an availability of a key in player's inventory to open a chest or a specific looking into the picture
    protected abstract bool PlayerCanInteract();

    public void ShowInteractionText()
    {
        if (PlayerCanInteract())
            _interactionTextMesh.gameObject.SetActive(true);
    }

    public void HideInteractionText()
    {
        _interactionTextMesh.gameObject.SetActive(false);
    }

    public abstract void Interact();
}
