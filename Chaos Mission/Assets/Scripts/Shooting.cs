using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    [RequireComponent(typeof(InputHandler))]
    public sealed class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _stonePrefab;
        [SerializeField] private float _throwForce = 13f;
        [SerializeField] private float _offset = 1f;

        private Camera _mainCamera;
        private InputHandler _inputHandler;

        #region UnityMethods

        private void Awake()
        {
            _mainCamera = Camera.main;
            _inputHandler = GetComponent<InputHandler>();
        }


        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _inputHandler.EnableAction(InputActions.Shooting);
            _inputHandler.AddHandler(InputActions.Shooting, OnShoot);
        }

        private void OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Shooting);
            _inputHandler.RemoveHandler(InputActions.Shooting, OnShoot);
        }

        #endregion


        private void OnShoot(InputAction.CallbackContext context)
        {
            var direction = GetNormalizedThrowVector();
            context.ReadValue<Vector3>();
            Throwable bullet = SpawnBullet(direction);
            ThrowBullet(bullet, direction);
        }

        private void ThrowBullet(Throwable bullet, Vector3 direction)
        {
            bullet.Throw(_throwForce * direction);
        }

        private Throwable SpawnBullet(Vector3 direction)
        {
            Vector3 spawnPosition = GetBulletSpawnPosition(direction);
            GameObject bullet = Instantiate(_stonePrefab, spawnPosition, Quaternion.identity, transform);
            return bullet.GetComponent<Throwable>();
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(//Mouse.current.position.ReadValue());
            mousePosition.z = 0f;
            return mousePosition;
        }

        private Vector3 GetBulletSpawnPosition(Vector3 direction)
        {
            Vector3 playerPosition = transform.position;
            return playerPosition + _offset * direction;
        }

        private Vector3 GetNormalizedThrowVector()
        {
            Vector3 mousePosition = GetMousePosition();
            Vector3 playerPosition = transform.position;
            return (mousePosition - playerPosition).normalized;
        }
    }
}
