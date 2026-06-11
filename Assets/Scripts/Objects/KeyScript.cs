using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KeyScript : MonoBehaviour
{
    [field: SerializeField] public KeyDefinition Definition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
