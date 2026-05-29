using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
    }
}
