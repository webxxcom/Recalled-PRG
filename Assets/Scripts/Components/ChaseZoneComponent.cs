using System.Collections.Generic;
using UnityEngine;

public class ChaseZoneComponent : MonoBehaviour
{
    [SerializeField] List<ChaseComponent> chasers;

    GameObject currentTarget;

    void AllStartChasing(GameObject target)
    {
        chasers.ForEach(e => e.Target = target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable target))
        {
            currentTarget = target.GameObject;

            AllStartChasing(currentTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable target))
        {
            if (currentTarget = target.GameObject)
            {
                currentTarget = null;

                AllStartChasing(currentTarget);
            }
        }
    }
}
