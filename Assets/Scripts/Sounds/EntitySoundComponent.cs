using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class EntitySoundComponent : MonoBehaviour
{
    protected AudioSource _audioSource;

    void Awake() => _audioSource = GetComponent<AudioSource>();
}
