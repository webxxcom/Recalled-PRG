using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class UIEventRaiser : MonoBehaviour
{
    [Header("Broadcasts to")]
    [SerializeField] GameobjectGameEvent OnUIElementSelected;
    [SerializeField] VoidGameEvent OnUIElementDeselected;

    GameObject _selectedObject;
    EventSystem _eventSystem;

    private void Awake()
        => _eventSystem = GetComponent<EventSystem>();

    private void Update()
    {
        if (_selectedObject != _eventSystem.currentSelectedGameObject)
        {
            _selectedObject = _eventSystem.currentSelectedGameObject;

            if (_eventSystem.currentSelectedGameObject != null)
                OnUIElementSelected.Invoke(_selectedObject);
            else OnUIElementDeselected.Invoke();
        }
    }
}
