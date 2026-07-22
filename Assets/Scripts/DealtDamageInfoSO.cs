using UnityEngine;

[CreateAssetMenu(fileName = "DealtDamageInfoSO", menuName = "Scriptable Objects/DealtDamageInfoSO")]
public class DealtDamageInfoSO : ScriptableObject
{
    [field: SerializeField] public GameObject Changer { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}
