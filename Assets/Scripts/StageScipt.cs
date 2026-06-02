using System.Linq;
using UnityEngine;

public class StageScipt : MonoBehaviour
{
    EnemyController[] enemies;
    StageDoorScript doorScript;

    private void Start()
    {
        enemies = GetComponentsInChildren<EnemyController>();
        doorScript = GetComponentInChildren<StageDoorScript>();
    }

    void OpenDoorIfAllEnemiesAreDead()
    {
        if (!doorScript.IsInteracted && enemies.Count(e => e.IsDead) == enemies.Length)
            doorScript.Open();
    }

    private void Update()
    {
        OpenDoorIfAllEnemiesAreDead();
    }

}
