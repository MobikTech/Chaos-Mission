using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    public sealed class Jumpable : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        
        private Rigidbody2D _rigidbody2D;
        private InputAction _jumpInputAction;
        
#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _jumpInputAction = InputHandler.InputCollection.Player.Jumping;
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _jumpInputAction.Enable();
            _jumpInputAction.performed += Jump;
        }

        private void OnDisable()
        {
            _jumpInputAction.Disable();
            _jumpInputAction.performed -= Jump;
        }

#endregion

        private void Jump(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            // Debug.Log("Jump");
        }
    }
}