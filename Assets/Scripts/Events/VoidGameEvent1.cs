using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/BossStartData Game Event")]
public class BossStartDataGameEvent : ScriptableObject
{
    public event Action<BossStartData> OnEventRaised;

    public void Invoke(BossStartData bsd) => OnEventRaised?.Invoke(bsd);
}
