using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData")]
public class DialogueData : ScriptableObject
{
    [field: SerializeField] public Sprite EntitySprite { get; private set; }
    [field: SerializeField] public TextAsset TextFile { get; private set; }
}
