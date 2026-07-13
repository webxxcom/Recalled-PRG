using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlobalStateManager : MonoBehaviour
{
    [SerializeField] InputActionAsset _inputActionAsset;
    [SerializeField] PlayerInput _playerInput;

    private void Start()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }
}
