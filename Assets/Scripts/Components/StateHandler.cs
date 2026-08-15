using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] StateStackHandler _stateManager;
    [SerializeField] GameState _definition;
    [SerializeField] VoidGameEvent OnGameEventRaised;
    [SerializeField] Canvas _uiCanvasRoot;

    bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value) Show();
            else Hide();

            _isActive = value;
        }
    }

    private void OnEnable()
        => OnGameEventRaised.OnEventRaised += Toggle;
    private void OnDisable()
        => OnGameEventRaised.OnEventRaised -= Toggle;
    void Toggle() => IsActive = !IsActive;

    void Show()
    {
        _uiCanvasRoot.enabled = true;
        _stateManager.Add(_definition);
    }

    void Hide()
    {
        _uiCanvasRoot.enabled = false;
        _stateManager.Remove();
    }
}
