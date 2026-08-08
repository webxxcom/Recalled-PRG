using UnityEngine;

[CreateAssetMenu(menuName = "Attack / Projectile Attack Data")]
public class ProjectileAttackDataSO : AttackSO
{
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public float NormalizedSpawnPoint { get; private set; }
}
