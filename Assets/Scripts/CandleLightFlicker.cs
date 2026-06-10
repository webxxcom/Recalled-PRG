using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class CandleLightFlicker : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }

    Light2D light2D;
    float baseIntensity;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        baseIntensity = light2D.intensity;
    }

    private void Update()
    {
        float slow = Mathf.PerlinNoise1D(Time.time * 0.5f) * 0.7f;
        float fast = Mathf.PerlinNoise1D(Time.time * 8f) * 0.3f;
        float noise = slow + fast; // roughly 0–1, weighted toward slow

        light2D.intensity = baseIntensity * Mathf.Lerp(0.3f, 1.0f, noise);
    }
}
