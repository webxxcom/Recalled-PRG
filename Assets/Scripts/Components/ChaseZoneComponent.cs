using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseZoneComponent : MonoBehaviour
{
    [SerializeField] List<ChaseComponent> chasers;

    readonly List<GameObject> currentTargets = new();

    public GameObject CurrentTarget { get => currentTargets.FirstOrDefault(); }

    void AllStartChasing(GameObject target)
    {
        chasers.ForEach(e => e.Target = target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable _))
        {
            currentTargets.Add(collision.gameObject);

            AllStartChasing(CurrentTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable _))
        {
            currentTargets.Remove(collision.gameObject);

            AllStartChasing(CurrentTarget);
        }
    }
}
