using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    public sealed class Movable : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        
        private Rigidbody2D _rigidbody2D;
        private InputAction _moveInputAction;
        
#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _moveInputAction = InputHandler.InputCollection.Player.Moving;
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _moveInputAction.Enable();
            _moveInputAction.performed += Move;
        }

        private void OnDisable()
        {
            _moveInputAction.Disable();
            _moveInputAction.performed -= Move;
        }

#endregion

        private void Move(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForce(context.ReadValue<float>() * Vector2.right * _moveSpeed, ForceMode2D.Force);
            // Debug.Log("Move");
        }
    }
}