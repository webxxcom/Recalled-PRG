using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Camera cam;

    void Start()
    {
        player = GameObject.Find("Player");
        cam = gameObject.GetComponent<Camera>();

        transform.position = new(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DungeonScene")
        {
            cam.backgroundColor = Color.black;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
    }

}
