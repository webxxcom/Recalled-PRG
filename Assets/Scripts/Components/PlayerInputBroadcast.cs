using UnityEngine;

public class PlayerInputBroadcast : MonoBehaviour
{
    [Header("Broadcasts to")]
    [SerializeField] VoidGameEvent OnInventory;
    [SerializeField] VoidGameEvent OnPauseMenu;

    void OnPause()
        => OnPauseMenu.Invoke();
    void OnToggleInventory()
        => OnInventory.Invoke();
}
