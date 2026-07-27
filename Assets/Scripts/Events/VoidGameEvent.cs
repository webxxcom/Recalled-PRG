using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Void Game Event")]
public class VoidGameEvent : ScriptableObject
{
    public event Action OnEventRaised;

    public void Invoke() => OnEventRaised?.Invoke();
}
