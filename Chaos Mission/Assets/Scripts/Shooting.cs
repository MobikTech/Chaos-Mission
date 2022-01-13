using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    public sealed class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _stonePrefab;
        [SerializeField] private float _throwForce = 13f;
        [SerializeField] private float _offset = 1f;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnShoot()
        {
            Flying bullet = SpawnBullet();
            bullet.Throw(_throwForce * direction.normalized);
        }

        private Flying SpawnBullet()
        {
            Vector3 spawnPosition = GetBulletSpawnPosition();
            GameObject stoneGameObject = Instantiate(_stonePrefab, spawnPosition, Quaternion.identity, transform);
            return stoneGameObject.GetComponent<Flying>();
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0f;
            return mousePosition;
        }

        private Vector3 GetBulletSpawnPosition()
        {
            Vector3 mousePosition = GetMousePosition();
            Vector3 playerPosition = transform.position;
            Vector3 direction = mousePosition - playerPosition;
            return playerPosition + _offset * direction.normalized;
        }
    }
}
