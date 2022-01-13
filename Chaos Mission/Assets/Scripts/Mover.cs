using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    [RequireComponent(typeof(InputHandler), typeof(Rigidbody2D))]
    public sealed class Mover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;

        private Rigidbody2D _rigidbody2D;
        private InputHandler _inputHandler;
     

#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputHandler = GetComponent<InputHandler>();
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _inputHandler.EnableAction(InputActions.Moving);
            _inputHandler.EnableAction(InputActions.Jumping);
            _inputHandler.AddHandler(InputActions.Moving, Move);
            _inputHandler.AddHandler(InputActions.Jumping, Jump);
        }

        private void OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Moving);
            _inputHandler.DisableAction(InputActions.Jumping);
            _inputHandler.RemoveHandler(InputActions.Moving, Move);
            _inputHandler.RemoveHandler(InputActions.Jumping, Jump);
        }

#endregion

        private void Move(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForce(context.ReadValue<float>() * Vector2.right * _moveSpeed, ForceMode2D.Force);
            // Debug.Log("Move");
        }
        
        private void Jump(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            // Debug.Log("Jump");
        }
    }
}