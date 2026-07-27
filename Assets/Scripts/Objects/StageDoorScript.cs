using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : InteractableObject
{
    new Collider2D collider2D;

    protected override void Awake()
    {
        base.Awake();

        collider2D = GetComponent<Collider2D>();
    }

    protected void Open()
    {
        IsInteracted = true;
        collider2D.enabled = false;
    }

    public override void Interact() => Open();
    protected override bool PlayerCanInteract() => true;
}

public class StageDoorScript : Door
{
    [Header("Listens to")]
    [SerializeField] VoidGameEvent OnStageCleared;

    //TODO
    //private void OnEnable() => OnStageCleared.OnEventRaised += Open;
    //private void OnDisable() => OnStageCleared.OnEventRaised -= Open;

    public override void Interact() { }
    protected override bool PlayerCanInteract() => true;
}
