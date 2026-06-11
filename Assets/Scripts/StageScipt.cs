using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StageScipt : MonoBehaviour
{
    [SerializeField] Light2D stageLight;
    EnemyController[] enemies;
    StageDoorScript doorScript;

    private void Start()
    {
        stageLight.enabled = false;
        enemies = GetComponentsInChildren<EnemyController>();
        doorScript = GetComponentInChildren<StageDoorScript>();
    }

    void IfAllEnemiesAreDead()
    {
        if (!doorScript.IsInteracted && enemies.Count(e => e.IsDead) == enemies.Length)
        {
            doorScript.Interact();
            stageLight.enabled = true;
            stageLight.GetComponent<Animator>().SetTrigger("Interact");
        }
    }

    private void Update()
    {
        IfAllEnemiesAreDead();
    }
}
