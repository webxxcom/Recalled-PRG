using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent HealthComponent { get; private set; }

    private void Awake()
    {
        HealthComponent = GetComponentInParent<HealthComponent>();
    }
}
