using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public abstract class DefaultDialogComponent : MonoBehaviour
{
    [field: SerializeField] public Image SpriteImage { get; set; }
    [field: SerializeField] public TextMeshProUGUI MainText { get; set; }
    [SerializeField] float _secondsToWait = 0.035f;
    public AudioSource AudioSource { get; private set; }

    protected virtual void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    protected IEnumerator RevealDialogueText()
    {
        MainText.maxVisibleCharacters = 0;

        AudioSource.Play();
        while (MainText.maxVisibleCharacters != MainText.text.Length)
        {
            MainText.maxVisibleCharacters += 1;

            yield return new WaitForSeconds(_secondsToWait);
        }

        AudioSource.Stop();
    }

    public abstract void BeginTalking(DialogueData dd);
}