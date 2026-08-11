using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    [SerializeField] List<Collider2D> _doors;

    [Header("Listens to")]
    [SerializeField] BossStartDataGameEvent OnBossStarted;

    private void Start()
    {
        DoorsState(false);
    }

    private void OnEnable()
    {
        OnBossStarted.OnEventRaised += BossFight;
    }
    private void OnDisable()
    {
        OnBossStarted.OnEventRaised -= BossFight;
    }

    void BossFight(BossStartData _)
    {
        DoorsState(true);
    }

    void DoorsState(bool isOpen)
    {
        foreach (var collider2D in _doors)
            collider2D.enabled = isOpen;
    }
}
