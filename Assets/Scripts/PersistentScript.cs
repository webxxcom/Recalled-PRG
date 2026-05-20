using UnityEngine;

public class PersistentScript : MonoBehaviour
{
    private static PersistentScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
