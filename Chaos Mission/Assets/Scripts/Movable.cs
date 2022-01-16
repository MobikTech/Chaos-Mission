using System;
using System.Collections;
using ChaosMission.Input;
using ChaosMission.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    [RequireComponent(typeof(InputHandler), typeof(Rigidbody2D))]
    public sealed class Movable : MonoBehaviour
    {
        [Header("Moving Setting")]
        [SerializeField] private float _maxMoveSpeed = 6f;
        [SerializeField, Range(0, 1)] private float _accelerationFactor = 0.3f;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _inputHandler;

        public bool OnMoving { get; private set; } = false;
        public Action MovingStarted;
        public Action MovingStopped;


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
            _inputHandler.AddHandler(InputActions.Moving, OnMove);
        }

        private void OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Moving);
            _inputHandler.RemoveHandler(InputActions.Moving, OnMove);
        }

#endregion

        private void OnMove(InputAction.CallbackContext context) => StartCoroutine(Moving(context));

        
        private IEnumerator Moving(InputAction.CallbackContext context)
        {
            OnMoving = true;
            WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

            TryFlip(context.ReadValue<float>());
            
            MovingStarted?.Invoke();
            while (context.phase == InputActionPhase.Performed)
            {
                Vector2 currentVelocity = _rigidbody2D.velocity;
                float xVelocityAddition = context.ReadValue<float>() * _accelerationFactor;
                _rigidbody2D.velocity = new Vector2(
                        Mathf.Clamp(currentVelocity.x + xVelocityAddition, -_maxMoveSpeed, _maxMoveSpeed), 
                        currentVelocity.y);
                
                yield return fixedUpdate;
            }
            
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            MovingStopped?.Invoke();
            OnMoving = false;
        }

        
        private void TryFlip(float keyAxisValue)
        {
            if (keyAxisValue == 0)
            {
                return;
            }

            Vector3 triggeredDirection = keyAxisValue < 0 ? Vector3.left : Vector3.right;
            transform.TryFlip(Axes.X, triggeredDirection);
        }
        
    }
}