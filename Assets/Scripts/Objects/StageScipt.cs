using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageScipt : MonoBehaviour
{
    [SerializeField] HealthProvider[] _enemies;

    [Header("Broadcasts on")]
    [SerializeField] VoidGameEvent OnStage1Cleared;

    private void Update()
    {
        // TODO
        if (_enemies.All(hp => hp == null))
        {
            if (OnStage1Cleared != null)
                OnStage1Cleared.Invoke();

            enabled = false;
        }
    }
}
