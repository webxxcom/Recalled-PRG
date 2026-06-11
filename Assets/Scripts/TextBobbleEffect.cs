using UnityEngine;

[RequireComponent(typeof(Transform))]
public class TextBobbleEffect : MonoBehaviour
{
    [field: SerializeField] public float Frequency { get; private set; }
    [field: SerializeField] public float Amplitude { get; private set; }

    Vector2 basePosition;
    new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        basePosition = transform.position;
    }

    void Update()
    {
        transform.position
            = new(basePosition.x,
                basePosition.y + Mathf.Sin(Time.time * Frequency) * Amplitude, 0);
    }
}
