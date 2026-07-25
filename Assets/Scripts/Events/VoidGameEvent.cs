using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Game Event")]
public class VoidGameEvent : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void Invoke() => OnEventRaised?.Invoke();
}
