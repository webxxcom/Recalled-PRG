using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class DialogueD
{
    [System.Serializable]
    public class Line
    {
        [System.Serializable]
        public class Choice
        {
            public string text;

            public int next;
        }

        public int id;
        public string text;
        public Choice[] choices;
        public int next;
    }

    public string speaker;
    public Line[] lines;
}


public class DialogueManager : MonoBehaviour
{
    [SerializeField] Button[] _playerButtons;

    PlayerInput _playerInput;
    LeftEntityDialogController _leftEntity;
    PlayerDialogueController _player;
    Canvas _canvas;
    DialogueD _dialogueData;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();

        _leftEntity = FindAnyObjectByType<LeftEntityDialogController>();
        _player = FindAnyObjectByType<PlayerDialogueController>();
        _playerInput = FindAnyObjectByType<PlayerInput>();
    }

    private void Start()
    {
        _canvas.enabled = false;
    }

    public void BeginDialogue(DialogueData dialogueData, bool left = false)
    {
        _dialogueData = JsonUtility.FromJson<DialogueD>(dialogueData.TextFile.text);
        _canvas.enabled = true;
        _playerInput.SwitchCurrentActionMap("UI");

        if (left) // TODO should separate the left entity sprite setting
            _leftEntity.SpriteImage.sprite = dialogueData.EntitySprite;

        StartCoroutine(BeginTalking());
    }

    IEnumerator RevealDialogueText(DefaultDialogComponent ddc)
    {
        ddc.MainText.maxVisibleCharacters = 0;

        ddc.AudioSource.Play();
        while (ddc.MainText.maxVisibleCharacters != ddc.MainText.text.Length)
        {
            ddc.MainText.maxVisibleCharacters += 1;

            yield return new WaitForSeconds(ddc.SecondsToWait);
        }

        ddc.AudioSource.Stop();
    }

    public PlayerDialogueButtonScript buttonPressedData;
    bool EnterPressed = false;
    IEnumerator BeginTalking()
    {
        var currentLine = _dialogueData.lines.First(); // The first line is always the opening line

        while (true)
        {
            _leftEntity.MainText.text = currentLine.text;

            // Wait until left entity stop talking or is out of space for letters
            yield return StartCoroutine(RevealDialogueText(_leftEntity));

            // If there are choices then wait until player chooses smth
            if (currentLine.choices != null)
            {
                PutPlayersChoices(currentLine.choices);

                yield return new WaitUntil(() => buttonPressedData != null);

                currentLine = GetNext(buttonPressedData.Choice.next);
                if (currentLine == null)
                {
                    Debug.Log("ERROR WITH DIALOG ID");
                }
            }
            // No choices but we have another line
            else if (currentLine.choices == null && currentLine.next != 0)
            {
                yield return new WaitUntil(() => EnterPressed);
                currentLine = GetNext(currentLine.next);
            }
            // No choices and the next id is 0 then stop dialogue
            else if (currentLine.choices == null && currentLine.next == 0)
            {
                EndTalking();
                yield break;
            }
        }
    }

    void OnSubmit(InputValue value)
    {
        EnterPressed = value.isPressed;
    }

    DialogueD.Line GetNext(int id)
    {
        foreach (var l in _dialogueData.lines)
        {
            if (l.id == id)
                return l;
        }
        return null;
    }

    void PutPlayersChoices(DialogueD.Line.Choice[] choices)
    {
        int i = 0;
        foreach (var choice in choices)
        {
            _playerButtons[i].GetComponent<PlayerDialogueButtonScript>().Choice = choice;
            _playerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = (++i) + ". " + choice.text;
        }
    }

    void EndTalking()
    {
        _canvas.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
    }
}
