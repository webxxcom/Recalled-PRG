using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateStackHandler : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameState _baseState;

    readonly Stack<GameState> _states = new();

    private void Awake() => Add(_baseState);

    void StateChanged()
    {
        GameState current = _states.Peek();

        _playerInput.SwitchCurrentActionMap(current.ActionMap);
        Time.timeScale = current.FreezeTime ? 0f : 1f;
        Cursor.lockState = current.CursorMode;
    }

    public void Add(GameState state)
    {
        _states.Push(state);
        StateChanged();
    }

    public void Remove()
    {
        _states.Pop();
        StateChanged();
    }
}
