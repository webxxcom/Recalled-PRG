using UnityEngine;

public class StageScript : MonoBehaviour
{
    [SerializeField] DoorsRuntimeSet _doors;
    [SerializeField] TransformRuntimeSet _enemies;

    [Header("Broadcasts to")]
    public VoidGameEvent OnStageCleared;

    private void OnEnable()
    {
        _enemies.OnChanged += CheckStageClear;
    }

    private void OnDisable()
    {
        _enemies.OnChanged -= CheckStageClear;
    }

    void CheckStageClear()
    {
        if (_enemies.Items.Count == 0)
        {
            OnStageCleared.Invoke();
            _doors.Items.ForEach(d => d.Open());
        }
    }
}
