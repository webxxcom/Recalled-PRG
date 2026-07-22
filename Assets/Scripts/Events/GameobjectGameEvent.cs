using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Gameobject Game Event")]
public class GameobjectGameEvent : ScriptableObject
{
    public event UnityAction<GameObject> OnEventRaised;

    public void Invoke(GameObject game) => OnEventRaised?.Invoke(game);
}
