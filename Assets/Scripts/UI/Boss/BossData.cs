using UnityEngine;

[System.Serializable]
public class BossData
{
    [field: SerializeField] public IntVariable Health { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}
