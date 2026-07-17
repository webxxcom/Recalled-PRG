using UnityEngine;

public class BarSwitchComponent : MonoBehaviour
{
    [SerializeField] HealthProvider _health;
    [SerializeField] GameObject _aliveBar;
    [SerializeField] GameObject _deadBar;

    void ToggleBars(GameObject _)
    {
        _aliveBar.SetActive(!_aliveBar.activeInHierarchy);
        _deadBar.SetActive(!_deadBar.activeInHierarchy);
    }

    private void Start()
    {
        _aliveBar.SetActive(!_health.IsDead);
        _deadBar.SetActive(_health.IsDead);
    }

    private void OnEnable()
    {
        _health.OnMinValueReached += ToggleBars;
    }

    private void OnDisable()
    {
        _health.OnMinValueReached -= ToggleBars;
    }
}
