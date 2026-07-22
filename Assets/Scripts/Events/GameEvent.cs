using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void Invoke() => OnEventRaised?.Invoke();
}
