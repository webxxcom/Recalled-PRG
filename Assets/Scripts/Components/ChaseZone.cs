using UnityEngine;

public class ChaseZone : MonoBehaviour
{
    public GameObject CurrentTarget { get; private set; }

    [Header("Broadcasts to")]
    [SerializeField] GameobjectGameEvent OnTargetEnteredTheZone;
    [SerializeField] GameobjectGameEvent OnTargetLeftTheZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentTarget = collision.gameObject;

            OnTargetEnteredTheZone.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentTarget = null;

            OnTargetLeftTheZone.Invoke(collision.gameObject);
        }
    }
}
