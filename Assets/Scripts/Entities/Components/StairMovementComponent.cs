using UnityEngine;

public class StairMovementComponent : MonoBehaviour
{
    public bool IsOnStairs { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
            IsOnStairs = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
            IsOnStairs = false;
    }

    public Vector2 ModifyMovementIfOnStairs(Vector2 movementDirection)
    {
        if (IsOnStairs)
        {
            movementDirection.y += -movementDirection.x * 0.8f;
            movementDirection.Normalize();
        }
        return movementDirection;
    }
}
