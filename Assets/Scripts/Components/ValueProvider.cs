using UnityEngine;

public abstract class ValueProvider : MonoBehaviour
{
    [SerializeField] ValueProviderConfig _config;
    [SerializeField] ValueProviderSO _valueProviderSO;

    // Lazy init SO
    public ValueProviderSO Value
    {
        get
        {
            if (_valueProviderSO == null)
                _valueProviderSO = ScriptableObject.CreateInstance<ValueProviderSO>();

            if (!_valueProviderSO.Initialized)
                _valueProviderSO.Init(_config);
            return _valueProviderSO;
        }
    }
}
