using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StageClearenceLight : MonoBehaviour
{
    private static readonly int InteractHash = Animator.StringToHash("Interact");
    Animator _animator;

    [Header("Listens to")]
    [SerializeField] VoidGameEvent OnStageCleared;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnStageCleared.OnEventRaised += Interact;
    }

    private void OnDisable()
    {
        OnStageCleared.OnEventRaised -= Interact;
    }

    void Interact()
    {
        _animator.SetTrigger(InteractHash);
    }
}
