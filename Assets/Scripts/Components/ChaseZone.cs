using System;
using UnityEngine;

public class ChaseZone : MonoBehaviour
{
    public GameObject CurrentTarget { get; private set; }

    public event Action<GameObject> OnTargetEnteredTheZone;
    public event Action<GameObject> OnTargetLeftTheZone;

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

            OnTargetLeftTheZone?.Invoke(collision.gameObject);
        }
    }
}
