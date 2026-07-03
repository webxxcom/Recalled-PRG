using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    LeftEntityDialogController _leftEntity;
    PlayerDialogueController _player;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();

        _leftEntity = FindAnyObjectByType<LeftEntityDialogController>();
        _player = FindAnyObjectByType<PlayerDialogueController>();
    }

    private void Start()
    {
        canvas.enabled = false;
    }

    public void BeginDialogue(DialogueData dialogueData)
    {
        canvas.enabled = true;

        _leftEntity.BeginTalking(dialogueData);
    }
}
