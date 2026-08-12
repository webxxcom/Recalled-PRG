using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ValueAggregator : MonoBehaviour
{
    private readonly List<float> _values = new();

    public void Add(float value) => _values.Add(value);
    public void Remove(float value) => _values.Remove(value);
    public float Get() => _values.Aggregate(1f, (acc, v) => acc * v);
}
