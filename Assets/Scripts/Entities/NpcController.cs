using UnityEngine;

public class NpcController : EntityController, IInteractable
{
    [SerializeField] DialogueData _dialogueData;

    DialogueManager _dialogueManager;

    protected override void Awake()
    {
        base.Awake();

        _dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    public void Interact()
    {
        _dialogueManager.BeginDialogue(_dialogueData);
    }
}
