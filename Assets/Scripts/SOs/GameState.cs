using UnityEngine;

[CreateAssetMenu(menuName = "Game States")]
public class GameState : ScriptableObject
{
    [field: SerializeField] public string ActionMap { get; private set; }
    [field: SerializeField] public CursorLockMode CursorMode { get; private set; }
    [field: SerializeField] public bool FreezeTime { get; private set; }
}