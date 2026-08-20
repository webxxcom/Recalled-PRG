using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected VoidGameEvent OnScreenGameEvent;

    Canvas _canvas;

    public bool IsActive
    {
        get => _canvas.enabled;
        private set
        {
            if (value) Open();
            else Close();

            _canvas.enabled = value;
        }
    }

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        //TODO  _canvas.enabled = false;
    }

    void ToggleScreen() => IsActive = !IsActive;
    protected virtual void OnEnable()
        => OnScreenGameEvent.OnEventRaised += ToggleScreen;
    protected virtual void OnDisable()
        => OnScreenGameEvent.OnEventRaised -= ToggleScreen;

    public abstract void Open();
    public abstract void Close();
}
