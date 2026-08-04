using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class DamageParticle : MonoBehaviour
{
    ParticleSystem _particleSystem;
    TextMeshProUGUI _textMeshPro;
    readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[10];

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        _particleSystem.GetParticles(_particles, 10);

        _textMeshPro.transform.position = _particles.FirstOrDefault().position;
    }
}
