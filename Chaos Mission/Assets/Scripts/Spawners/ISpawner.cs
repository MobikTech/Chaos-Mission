using System;
using UnityEngine;

namespace ChaosMission.Spawners
{
    public interface ISpawner
    {
        public void Spawn(Action<Vector3> instantiate);
    }
}