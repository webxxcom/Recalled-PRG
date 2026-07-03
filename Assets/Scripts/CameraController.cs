using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    GameObject player;
    new Camera camera;

    private void Awake()
    {
        player = GameObject.Find("Player");
        camera = GetComponent<Camera>();
    }

    void Start()
    {
        transform.position = new(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
