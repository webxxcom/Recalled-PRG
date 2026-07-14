using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    public HealthProvider Health { get; private set; }

    private void Awake()
    {
        Health = Utils.FindOrThrow(GetComponentInParent<HealthProvider>);
    }
}
