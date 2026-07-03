using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("Player").transform.position = gameObject.transform.position;
    }
}
