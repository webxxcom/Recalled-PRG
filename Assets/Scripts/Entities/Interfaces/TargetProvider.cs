using System.ComponentModel;
using UnityEngine;

public abstract class TargetProvider : MonoBehaviour
{
    public bool HasTarget => CurrentTarget != null;

    [field: SerializeField][ReadOnly(true)] public GameObject CurrentTarget { get; protected set; }
}
