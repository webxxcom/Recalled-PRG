using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour, IDamageInfoGameEventListener
{
    [SerializeField] private DamageInfoGameEvent Event;
    [SerializeField] private UnityEvent<DamageInfo> Response;

    private void OnEnable() => Event.RegisterListener(this);
    private void OnDisable() => Event.UnregisterListener(this);

    public void OnEventRaised(DamageInfo di) => Response.Invoke(di);
}
