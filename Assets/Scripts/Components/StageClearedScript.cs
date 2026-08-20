using UnityEngine;

/// <summary>
/// A stage is defined as a set of enemies to defeat with the doors to open after all enemies been defeated.
/// The doors which are unlocked will be locked and vice versa because some doors define the stage bounds for player not to flee
///     and some doors define the stage completion to unlock. Should separate it kinda but meh
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class StageScript : MonoBehaviour
{
    [SerializeField] DoorsRuntimeSet _doors;
    [SerializeField] TransformRuntimeSet _enemies;
    [SerializeField] Collider2D _detectionZone;

    [Header("Broadcasts to")]
    public VoidGameEvent OnStageStarted;
    public VoidGameEvent OnStageCleared;

    Collider2D _collider2D;

    private void Awake()
        => _collider2D = GetComponent<Collider2D>();
    private void OnEnable()
        => _enemies.OnChanged += CheckStageClear;
    private void OnDisable()
        => _enemies.OnChanged -= CheckStageClear;

    void CheckStageClear()
    {
        if (_enemies.Items.Count == 0)
        {
            _doors.Items.ForEach(d => d.Open());
            OnStageCleared.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnStageStarted.Invoke();
            _doors.Items.ForEach(d => d.Close());
            _collider2D.enabled = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_detectionZone == null)
            _detectionZone = GetComponent<Collider2D>();
    }
#endif
}
