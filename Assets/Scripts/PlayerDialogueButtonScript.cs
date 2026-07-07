using UnityEngine;

public class PlayerDialogueButtonScript : MonoBehaviour
{
    public DialogueD.Line.Choice Choice { get; set; }

    public void OnClick()
    {
        FindAnyObjectByType<DialogueManager>().buttonPressedData = this;
    }
}
