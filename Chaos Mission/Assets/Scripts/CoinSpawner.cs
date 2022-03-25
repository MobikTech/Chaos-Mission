using System;
using System.Linq;
using UnityEngine;
using ChaosMission.Spawners;

namespace ChaosMission
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab = null!;
        [SerializeField] private GameObject[] _spawnObjects = null!;
        private ISpawner _spawner = null!;
        
        private void Start()
        {
            Vector3[] spawnPosition = _spawnObjects
                .Select(spawnObject => spawnObject.transform.position)
                .ToArray();
            _spawner = new ShuffleSpawner(spawnPosition);
            _spawner.Spawn(InstantiateCoin);
        }
        
        private void InstantiateCoin(Vector3 position)
        {
            Instantiate(_coinPrefab, position, Quaternion.identity, gameObject.transform);
        }
    }
}
