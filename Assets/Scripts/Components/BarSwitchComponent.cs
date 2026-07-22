using UnityEngine;

public class BarSwitchComponent : MonoBehaviour
{
    [SerializeField] GameObject _aliveBar;
    [SerializeField] GameObject _deadBar;
    [SerializeField] HealthProvider _healthProvider;

    void ToggleBars(GameObject _)
    {
        _aliveBar.SetActive(!_aliveBar.activeInHierarchy);
        _deadBar.SetActive(!_deadBar.activeInHierarchy);
    }

    private void OnEnable()
    {
        _healthProvider.Health.OnMinValueReached += ToggleBars;
    }

    private void OnDisable()
    {
        _healthProvider.Health.OnMinValueReached -= ToggleBars;
    }
}
