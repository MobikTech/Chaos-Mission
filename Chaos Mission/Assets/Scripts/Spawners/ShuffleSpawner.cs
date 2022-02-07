using System;
using ChaosMission.Extensions;
using UnityEngine;

namespace ChaosMission.Spawners
{
    public class ShuffleSpawner : ISpawner
    {
        private readonly Vector3[] _spawnPositions;
        private int _currentIndex = 0;

        public ShuffleSpawner(Vector3[] spawnPositions)
        {
            _spawnPositions = spawnPositions;
            _spawnPositions.Shuffle();
        }

        public void Spawn(Action<Vector3> instantiate)
        {
            if (_currentIndex >= _spawnPositions.Length)
            {
                _spawnPositions.Shuffle();
                _currentIndex = 0;
            }
            
            instantiate(_spawnPositions[_currentIndex]);
            _currentIndex++;
        }
    }
}