using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    public Collider2D Collider2D { get; private set; }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }
}
