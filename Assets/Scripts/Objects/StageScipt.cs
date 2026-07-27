using System.Linq;
using UnityEngine;

public class StageScipt : MonoBehaviour
{
    EntityController[] _enemies;

    [Header("Broadcasts on")]
    [SerializeField] VoidGameEvent OnStage1Cleared;

    private void Start()
    {
      
    }

    private void Update()
    {
        // TODO
        //if (_enemies.Count(e => e.HealthProvider.IsDead) == _enemies.Length)
        //{
        //    if (OnStage1Cleared != null)
        //        OnStage1Cleared.Invoke();

        //    enabled = false;
        //}
    }
}
