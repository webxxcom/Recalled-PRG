using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EntityAudioController : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    List<EntitySoundComponent> soundComponents;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        soundComponents = GetComponents<EntitySoundComponent>().ToList();
    }

    private void OnEnable()
    {
        foreach (var sc in soundComponents)
        {
            if (sc.enabled)
            {
                sc.Activate();
            }
        }
    }

    private void OnDisable()
    {
        soundComponents.ForEach(s => s.Deactivate());
    }
}
