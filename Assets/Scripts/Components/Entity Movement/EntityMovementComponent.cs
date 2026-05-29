using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IMovementStrategy))]
public class EntityMovementComponent : MonoBehaviour
{
    public Vector2 LastMovement { get; private set; }
    public Vector2 Movement { get; private set; }
    public bool IsWalking => Movement != Vector2.zero;
    public float CurrentSpeed => WalkingSpeed;

    [field: SerializeField] public float WalkingSpeed { get; private set; }

    ITargetProvider[] targetProviders;
    IMovementStrategy movementStrategy;

    private void Awake()
    {
        movementStrategy = GetComponent<IMovementStrategy>();
        targetProviders = GetComponents<ITargetProvider>().OrderBy(o => o.Priority).ToArray();
    }

    public Vector2 GetFinalMovement()
    {
        Vector2 finalMovement = Vector2.zero;

        foreach (var item in targetProviders)
        {
            if (item.HasTarget)
            {
                if (item.CurrentTarget.TryGetComponent(out EntityController ec) && ec.IsDead)
                    continue;

                finalMovement = movementStrategy.GetDirection(item.CurrentTarget);
                break;
            }
        }
        return finalMovement * WalkingSpeed;
    }
}
