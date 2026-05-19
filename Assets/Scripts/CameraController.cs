using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        transform.position = new (player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
