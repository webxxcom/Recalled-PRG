using System;
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
        light2D.intensity = baseIntensity * Mathf.PerlinNoise1D(Time.time * Speed);
    }
}
