using UnityEngine;

public class WanderingMovementComponent : MovementStrategy
{
    [field: SerializeField] public Collider2D WanderingZone { get; private set; }

    private static readonly Vector2[] Directions =
        {
            Vector2.right,
            Vector2.left,
            Vector2.down,
            Vector2.up
        };

    public Vector2 IntermediateDirection
    {
        get => _IntermediateDirection;
        private set
        {
            _IntermediateDirection = value;
            timeSinceChangingDirection = 0;
        }
    }
    float timeSinceChangingDirection = 0;
    Vector2 _IntermediateDirection;
    bool isIdle = false;
    float CurrentStateDuration = 0f;

    public override Vector2 GetDirection(GameObject _) => IntermediateDirection;

    void FindNewDirection()
    {
        Vector2 newDirection;

        do
        {
            newDirection = Directions[Random.Range(0, 4)];
        } while (newDirection == IntermediateDirection);

        IntermediateDirection = newDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindNewDirection();
    }

    void AdvanceState()
    {
        CurrentStateDuration = Random.Range(0.6f, 2.3f);
        isIdle = !isIdle;

        if (!isIdle)
            FindNewDirection();
        else
            IntermediateDirection = Vector2.zero;
    }

    bool ShouldChangeDirection => timeSinceChangingDirection >= CurrentStateDuration;
    private void Update()
    {
        if (ShouldChangeDirection)
            AdvanceState();

        timeSinceChangingDirection += Time.deltaTime;
    }
}
