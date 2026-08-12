using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StageStartedScript : MonoBehaviour
{
    [SerializeField] DoorsRuntimeSet _doors;
    [SerializeField] Collider2D _detectionZone;

    [Header("Broadcasts to")]
    public VoidGameEvent OnStageStarted;

    Collider2D _collider2D;

    private void Awake()
        => _collider2D = GetComponent<Collider2D>();

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
