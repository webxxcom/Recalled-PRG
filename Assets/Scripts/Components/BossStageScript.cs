using UnityEngine;

[RequireComponent(typeof(StageScript))]
public class BossStageScript : MonoBehaviour
{
    [Header("Broadcasts to")]
    [SerializeField] BossStartDataGameEvent OnBossStart;

    StageScript _stageScript;

    private void Awake()
        => _stageScript = GetComponent<StageScript>();
    private void OnEnable()
        => _stageScript.OnStageCleared.OnEventRaised += Sta;
    private void OnDisable()
        => _stageScript.OnStageCleared.OnEventRaised -= Sta;

    void Sta()
    {

    }
}
