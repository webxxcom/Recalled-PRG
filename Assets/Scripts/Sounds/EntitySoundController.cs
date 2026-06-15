using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityAudioController : MonoBehaviour
{
    List<EntitySoundComponent> soundComponents;

    private void Awake()
    {
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
