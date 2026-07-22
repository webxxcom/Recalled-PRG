using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Gameobject Int Game Event")]
public class GameobjectIntGameEvent : ScriptableObject
{
    public event Action<GameObject, int> OnEventRaised;

    public void Invoke(GameObject game, int val) => OnEventRaised?.Invoke(game, val);
}
