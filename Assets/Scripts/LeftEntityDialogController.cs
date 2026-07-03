using UnityEngine;

public class LeftEntityDialogController : DefaultDialogComponent
{
    public override void BeginTalking(DialogueData dd)
    {
        MainText.SetText(dd.TextFile.text);
        SpriteImage.sprite = dd.EntitySprite;

        StartCoroutine(RevealDialogueText());
    }
}
