using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSource", menuName = "DialogueSource")]
public class DialogueSource : ScriptableObject
{
    [field: SerializeField] public Sprite EntitySprite { get; private set; }
    [field: SerializeField] public TextAsset TextFile { get; private set; }
}
