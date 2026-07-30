using UnityEngine;

public class NpcController : EntityController, IInteractable
{
    [SerializeField] DialogueSource _dialogueSource;

    [Header("Broadcasts to")]
    [SerializeField] DialogueSourceGameEvent OnDialogueStarted;

    public void Interact()
    {
        OnDialogueStarted.Invoke(_dialogueSource);
    }
}
