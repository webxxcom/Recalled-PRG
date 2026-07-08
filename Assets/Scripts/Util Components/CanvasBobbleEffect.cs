using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CanvasBobbleEffect : MonoBehaviour
{
    [field: SerializeField] public float Frequency { get; private set; } = 5.5f;
    [field: SerializeField] public float Amplitude { get; private set; } = 0.07f;
    [field: SerializeField] public DirectionEnum Direction { get; private set; }

    public enum DirectionEnum { Vertical, Horizontal }

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
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;

        if (Direction == DirectionEnum.Vertical)
            transform.position = new(basePosition.x, basePosition.y + offset, 0);
        else if (Direction == DirectionEnum.Horizontal)
            transform.position = new(basePosition.x + offset, basePosition.y, 0);
    }
}
