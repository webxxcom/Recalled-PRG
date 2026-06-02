using UnityEngine;

public class BarSwitchComponent : MonoBehaviour
{
    [field: SerializeField] public GameObject AliveBar { get; private set; }
    [field: SerializeField] public GameObject DeadBar { get; private set; }

    [SerializeField] HealthComponent healthComponent;

    void EnableAlivebar(bool flag)
    {
        AliveBar.SetActive(flag);
        DeadBar.SetActive(!flag);
    }

    private void Start()
    {
        healthComponent.OnMinValueReached += (_) => EnableAlivebar(false);
    }
}
