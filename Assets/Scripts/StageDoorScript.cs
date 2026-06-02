using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StageDoorScript : InteractableObjectScript
{
    new Collider2D collider2D;

    new private void Awake()
    {
        base.Awake();

        collider2D = GetComponent<Collider2D>();
    }

    public void Open()
    {
        IsInteracted = true;
        collider2D.enabled = false;
    }
}
