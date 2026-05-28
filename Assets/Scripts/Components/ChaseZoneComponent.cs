using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseZoneComponent : MonoBehaviour
{
    readonly List<GameObject> currentTargets = new();

    public GameObject CurrentTarget { get => currentTargets.FirstOrDefault(); }

    public event Action<GameObject> OnTargetEnteredTheZone;
    public event Action OnTargetLeftTheZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable _))
        {
            currentTargets.Add(collision.gameObject);

            OnTargetEnteredTheZone?.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable _))
        {
            currentTargets.Remove(collision.gameObject);

            OnTargetLeftTheZone?.Invoke();
        }
    }
}
