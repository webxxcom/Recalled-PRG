using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;

    void Start()
    {
        Instantiate(_playerPrefab, transform);
    }
}
