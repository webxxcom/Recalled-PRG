using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventRaiser : MonoBehaviour
{
    public GameObject SelectedObject { get; private set; }

    EventSystem _eventSystem;

    public Action<GameObject> OnUIElementSelected;
    public Action OnUIElementDeselected;

    private void Awake()
    {
        _eventSystem = Utils.FindOrThrow(FindAnyObjectByType<EventSystem>);
    }

    private void Update()
    {
        if (_eventSystem.currentSelectedGameObject && SelectedObject != _eventSystem.currentSelectedGameObject)
        {
            SelectedObject = _eventSystem.currentSelectedGameObject;

            OnUIElementSelected?.Invoke(SelectedObject);
        }
        else if (!_eventSystem.currentSelectedGameObject && SelectedObject)
        {
            OnUIElementDeselected?.Invoke();
            SelectedObject = null;
        }
    }
}
