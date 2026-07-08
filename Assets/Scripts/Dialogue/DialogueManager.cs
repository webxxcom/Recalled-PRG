using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Object _playerDialogueButtonPrefab;
    [SerializeField] GameObject _buttonGrid;
    [SerializeField] Button _continueButton;

    PlayerInput _playerInput;
    LeftEntityDialogController _leftEntity;
    PlayerDialogueController _player;
    Canvas _canvas;
    DialogueData _dialogueData;
    List<Button> _playerButtons;
    ConsumableValue<bool >_enterPressed;
    ConsumableValue<DialogueData.Line.Choice> _buttonPressedData;

    void ResetFields()
    {
        _continueButton.gameObject.SetActive(false);
        _buttonPressedData.Value = null;
        _enterPressed.Value = false;
    }

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();

        _leftEntity = FindAnyObjectByType<LeftEntityDialogController>();
        _player = FindAnyObjectByType<PlayerDialogueController>();
        _playerInput = FindAnyObjectByType<PlayerInput>();

        _continueButton.onClick.AddListener(() => _enterPressed.Value = true);
    }

    private void Start()
    {
        _canvas.enabled = false;
        ResetFields();
    }

    public void BeginDialogue(DialogueSource dialogueData, bool left = false)
    {
        _dialogueData = JsonUtility.FromJson<DialogueData>(dialogueData.TextFile.text);
        _canvas.enabled = true;
        _playerInput.SwitchCurrentActionMap("UI");
        _enterPressed.Value = false;
        _buttonPressedData.Value = null;
        
        if (left) // TODO should separate the left entity sprite setting
            _leftEntity.SpriteImage.sprite = dialogueData.EntitySprite;

        StartCoroutine(BeginTalking());
    }

    IEnumerator RevealDialogueText(DefaultDialogComponent ddc, string text)
    {
        ddc.AudioSource.Play();

        yield return Utils.RevealTextOverTime(ddc.MainText, ddc.DelayTime, text, ddc.MaxTextLength);

        ddc.AudioSource.Stop();
    }

    IEnumerator WaitDialogueInput()
    {
        _continueButton.gameObject.SetActive(true);

        yield return new WaitUntil(() => _enterPressed.Consume());

        _continueButton.gameObject.SetActive(false);
    }


    IEnumerator BeginTalking()
    {
        var currentLine = _dialogueData.lines.First(); // The first line is always the opening line

        while (true)
        {
            // Wait until left entity stop talking or is out of space for letters
            yield return StartCoroutine(RevealDialogueText(_leftEntity, currentLine.text));

            switch (currentLine.Type)
            {
                case DialogueData.Line.Types.Choices:
                    yield return StartCoroutine(PutPlayersChoices(currentLine.choices));

                    yield return new WaitUntil(() => _buttonPressedData.Value != null);

                    currentLine = GetNext(_buttonPressedData.Consume().next);
                    if (currentLine == null)
                    {
                        Debug.Log("ERROR WITH DIALOG ID");
                    }
                    break;

                case DialogueData.Line.Types.Continue:
                    // Wait for player continue button push;
                    yield return StartCoroutine(WaitDialogueInput());

                    currentLine = GetNext(currentLine.next);
                    break;

                case DialogueData.Line.Types.End:
                    yield return StartCoroutine(WaitDialogueInput());

                    EndTalking();

                    yield break;
            }
        }
    }

    DialogueData.Line GetNext(int id)
    {
        foreach (var l in _dialogueData.lines)
        {
            if (l.id == id)
                return l;
        }
        return null;
    }

    IEnumerator PutPlayersChoices(DialogueData.Line.Choice[] choices)
    {
        int i = 0;

        _playerButtons = new();
        foreach (var choice in choices)
        {
            _playerButtons.Add(Instantiate(
                _playerDialogueButtonPrefab, Vector3.zero, Quaternion.identity, _buttonGrid.transform).GetComponent<Button>());

            _playerButtons.Last().onClick.AddListener(() => _buttonPressedData.Value = choice);

            yield return StartCoroutine(Utils.RevealTextOverTime(
                _playerButtons.Last().GetComponentInChildren<TextMeshProUGUI>(),
                _player.DelayTime,
                (i + 1) + ". " + choice.text,
                200)
                );
            ++i;
        }
    }

    void EndTalking()
    {
        _canvas.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
        ResetFields();
    }
}
