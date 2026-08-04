using UnityEngine;

public class BarSwitchComponent : MonoBehaviour
{
    [SerializeField] GameObject _aliveBar;
    [SerializeField] GameObject _deadBar;
    [SerializeField] HealthProvider _healthProvider;

    void ToggleBars(DamageInfo _)
    {
        _aliveBar.SetActive(!_aliveBar.activeInHierarchy);
        _deadBar.SetActive(!_deadBar.activeInHierarchy);
    }

    private void OnEnable()
    {
        _healthProvider.OnMinValue += ToggleBars;
    }

    private void OnDisable()
    {
        _healthProvider.OnMinValue -= ToggleBars;
    }
}
