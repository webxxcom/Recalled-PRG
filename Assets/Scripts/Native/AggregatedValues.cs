using System.Collections.Generic;
using System.Linq;

public class AggregatedValue
{
    private readonly List<float> _values = new();   

    public void Add(float value) => _values.Add(value);
    public void Remove(float value) => _values.Remove(value);
    public float Get() => _values.Aggregate(1f, (acc, v) => acc * v);
}
