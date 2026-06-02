using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class InteractableObjectScript : MonoBehaviour
{
    private static readonly int InteractHash = Animator.StringToHash("Interact");

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
    SpriteRenderer spriteRenderer;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
