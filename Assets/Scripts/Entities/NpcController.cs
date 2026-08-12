using System;
using UnityEngine;

public class NpcController : EntityController, IInteractable
{
    [SerializeField] DialogueSource _dialogueSource;

    [Header("Broadcasts to")]
    [SerializeField] DialogueSourceGameEvent OnDialogueStarted;

    public event Action OnInteract;

    public void Interact()
    {
        OnDialogueStarted.Invoke(_dialogueSource);
        OnInteract?.Invoke();
    }
}
