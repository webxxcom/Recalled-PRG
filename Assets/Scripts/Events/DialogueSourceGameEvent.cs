using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Dialogue Hitbox Game Event")]
public class DialogueSourceGameEvent : ScriptableObject
{
    public event Action<DialogueSource> OnEventRaised;

    public void Invoke(DialogueSource dd) => OnEventRaised?.Invoke(dd);
}
