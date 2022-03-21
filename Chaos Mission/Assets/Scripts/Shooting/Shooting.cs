using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Handlers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Shooting
{
    public sealed class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletsParent;
        [SerializeField] private float _throwForce = 13f;
        [SerializeField] private float _offset = 1f;

        private Camera _mainCamera;
        private InputAction _shootingAction;

#region UnityMethods

        private void Awake()
        {
            _mainCamera = Camera.main;
            _shootingAction = new PlayerHandler().GetByType(PlayerActions.Shooting);
        }


        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _shootingAction.performed += OnShoot;
            _shootingAction.Enable();
        }

        private void OnDisable()
        {
            _shootingAction.Disable();
            _shootingAction.performed -= OnShoot;
        }

#endregion


        private void OnShoot(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = GetScreenMousePosition();
            var direction = GetNormalizedThrowVector(mousePosition);
            Throwable bullet = SpawnBullet(direction);
            ThrowBullet(bullet, direction);
        }

        private Vector3 GetScreenMousePosition()
        {
            return Mouse.current.position.ReadValue();
        }

        private void ThrowBullet(Throwable bullet, Vector3 direction)
        {
            bullet.Throw(_throwForce * direction);
        }

        private Throwable SpawnBullet(Vector3 direction)
        {
            Vector3 spawnPosition = GetBulletSpawnPosition(direction);
            GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity, _bulletsParent);
            return bullet.GetComponent<Throwable>();
        }

        private Vector3 GetWorldMousePosition(Vector3 mousePosition)
        {
            Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldMousePosition.z = 0f;
            return worldMousePosition;
        }

        private Vector3 GetBulletSpawnPosition(Vector3 direction)
        {
            Vector3 playerPosition = transform.position;
            return playerPosition + _offset * direction;
        }

        private Vector3 GetNormalizedThrowVector(Vector3 mousePosition)
        {
            Vector3 worldMousePosition = GetWorldMousePosition(mousePosition);
            Vector3 playerPosition = transform.position;
            return (worldMousePosition - playerPosition).normalized;
        }
    }
}
