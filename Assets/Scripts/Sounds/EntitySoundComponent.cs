using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class EntitySoundComponent : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    protected virtual void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public abstract void Activate();

    public abstract void Deactivate();
}
