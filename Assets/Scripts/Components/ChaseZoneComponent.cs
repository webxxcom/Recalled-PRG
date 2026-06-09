using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseZoneComponent : MonoBehaviour
{
    public GameObject CurrentTarget { get; private set; }

    public event Action<GameObject> OnTargetEnteredTheZone;
    public event Action OnTargetLeftTheZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentTarget = collision.gameObject;

            OnTargetEnteredTheZone?.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentTarget = null;

            OnTargetLeftTheZone?.Invoke();
        }
    }
}
